#include <algorithm>
#include <cmath>
#include <cstdio>
#include <iostream>
#include <map>
#include <sstream>
#include <string>
#include <vector>
using namespace std;

int const oo = +1000000000;

inline void __assert(bool condition, string message) {
      if (!condition) {
            throw message;
      }
}

struct mirror {
      int row, col, type;
      int row_location;
      int col_location;
      mirror(int row, int col, int type) {
            this->row = row;
            this->col = col;
            this->type = type;
      }
      bool operator <(mirror const &other) const {
            if (row != other.row) {
                  return (row < other.row);
            }
            return (col < other.col);
      }
};

struct segment {
      int begx, begy;
      int endx, endy;
      segment(int begx, int begy, int endx, int endy) {
            this->begx = begx;
            this->begy = begy;
            this->endx = endx;
            this->endy = endy;
      }
};

struct event {
      int position;
      int type;
      segment* segm;
      event(int position, int type, segment* segm) {
            this->position = position;
            this->type = type;
            this->segm = segm;
      }
      bool operator <(event const &other) const {
            if (position != other.position) {
                  return (position < other.position);
            }
            return (type > other.type);
      }
};

class fenwick {
private:
      vector<int> cumul;
private:
      int sum(int pos) {
            int result = 0;
            while (pos > 0) {
                  result += cumul[pos];
                  pos -= pos & (-pos);
            }
            return result;
      }
public:
      fenwick(int n) {
            cumul.assign(n + 1, 0);
      }
      void inc(int pos, int value) {
            while (pos < cumul.size()) {
                  cumul[pos] += value;
                  pos += pos & (-pos);
            }
      }
      int sum(int lo, int hi) {
            return sum(hi) - sum(lo - 1);
      }
      int idx(int lo, int hi) {
            if (lo + 8 < hi) {
                  int xx = (lo + hi) >> 1;
                  if (sum(lo, xx) != 0) return idx(lo, xx);
                  if (sum(xx, hi) != 0) return idx(xx, hi);
            }
            else {
                  for (int i = lo; i <= hi; ++i) {
                        if (sum(i, i) != 0) {
                              return i;
                        }
                  }
            }
            return -1;
      }
};

class processor {
private:
      map<int, vector<mirror*>* > mirrors_by_row;
      map<int, vector<mirror*>* > mirrors_by_col;
      int nrows, ncols;
private:
      int is_not_fake_mirror(mirror* const &where) {
            if (where) {
                  return (1 <= where->row && where->row <= nrows) &&
                         (1 <= where->col && where->col <= ncols);
            }
            return 0;
      }
      mirror* get_next_mirror(vector<mirror*> const &mirrors, int location, int dir) {
            if (dir != 0) {
                  location += dir;
                  if (0 <= location && location < mirrors.size()) {
                        return mirrors[location];
                  }
            }
            return 0;
      }
      vector<mirror> trace_beam(mirror* curr, int dx, int dy) {
            vector<mirror> result;
            while (true) {
                  result.push_back(*curr);
                  mirror* next;
                  if (dx != 0) next = get_next_mirror(*mirrors_by_row[curr->row], curr->col_location, dx * curr->type);
                  if (dy != 0) next = get_next_mirror(*mirrors_by_col[curr->col], curr->row_location, dy * curr->type);
                  if (is_not_fake_mirror(next)) {
                        std::swap(dx, dy);
                        dx *= curr->type;
                        dy *= curr->type;
                        curr = next;
                  }
                  else {
                        if (next) {
                              result.push_back(*next);
                        }
                        else {
                              if (dx != 0) {
                                    if (curr->type > 0) result.push_back(mirror(curr->row, (dx > 0 ? ncols + 1 : 0), -1));
                                    if (curr->type < 0) result.push_back(mirror(curr->row, (dx > 0 ? 0 : ncols + 1), -1));
                              }
                              if (dy != 0) {
                                    if (curr->type > 0) result.push_back(mirror((dy > 0 ? nrows + 1 : 0), curr->col, -1));
                                    if (curr->type < 0) result.push_back(mirror((dy > 0 ? 0 : nrows + 1), curr->col, -1));
                              }
                        }
                        break;
                  }
            }
            return result;
      }
      int clamp(int &begx, int &begy, int &endx, int &endy) {
            if (abs(begx - endx) + abs(begy - endy) > 1) {
                  if (begx != endx) {
                        int sign = (begx < endx ? -1 : begx > endx ? +1 : 0);
                        begx -= sign;
                        endx += sign;
                  }
                  if (begy != endy) {
                        int sign = (begy < endy ? -1 : begy > endy ? +1 : 0);
                        begy -= sign;
                        endy += sign;
                  }
                  return 1;
            }
            return 0;
      }
      vector<segment> make_segments(vector<mirror> const &mirrors, int hor) {
            vector<segment> result;
            for (size_t i = 1; i < mirrors.size(); ++i) {
                  int begx = mirrors[i - 1].row, begy = mirrors[i - 1].col;
                  int endx = mirrors[i - 0].row, endy = mirrors[i - 0].col;
                  int ok = (begy == endy && begx != endx);
                  if (hor) {
                        ok = (begx == endx && begy != endy);
                  }
                  if (ok) {
                        if (clamp(begx, begy, endx, endy)) {
                              result.push_back(segment(min(begx, endx), min(begy, endy), max(begx, endx), max(begy, endy)));
                        }
                  }
            }
            return result;
      }
      long long get_positions_count(vector<segment> hor, vector<segment> ver, int &lexminx, int &lexminy) {
            vector<event> events;
            for (size_t i = 0; i < hor.size(); ++i) {
                  events.push_back(event(hor[i].begy, +1, &hor[i]));
                  events.push_back(event(hor[i].endy, -1, &hor[i]));
            }
            for (size_t i = 0; i < ver.size(); ++i) {
                  events.push_back(event(ver[i].begy, +0, &ver[i]));
            }
            sort(events.begin(), events.end());
            fenwick* cumul = new fenwick(nrows);
            long long result = 0;
            for (size_t i = 0; i < events.size(); ++i) {
                  if (events[i].type == 0) {
                        int by = cumul->sum(events[i].segm->begx, events[i].segm->endx);
                        if (by > 0) {
                              result += by;
                              int row = cumul->idx(events[i].segm->begx, events[i].segm->endx);
                              if (row < lexminx) {
                                    lexminx = row;
                                    lexminy = events[i].position;
                              }
                        }
                  }
                  else {
                        cumul->inc(events[i].segm->begx, events[i].type);
                  }
            }
            return result;
      }
public:
      processor(int nrows, int ncols) {
            this->nrows = nrows;
            this->ncols = ncols;
      }
      string process(vector<mirror> &mirrors) {
            mirrors.push_back(mirror(1, 0, +1));
            mirrors.push_back(mirror(nrows, ncols + 1, -1));
            sort(mirrors.begin(), mirrors.end());
            for (size_t i = 0; i < mirrors.size(); ++i) {
                  vector<mirror*>* &row_ref = mirrors_by_row[mirrors[i].row];
                  vector<mirror*>* &col_ref = mirrors_by_col[mirrors[i].col];
                  if (!row_ref) row_ref = new vector<mirror*>();
                  if (!col_ref) col_ref = new vector<mirror*>();
                  row_ref->push_back(&mirrors[i]);
                  col_ref->push_back(&mirrors[i]);
                  mirrors[i].col_location = row_ref->size() - 1;
                  mirrors[i].row_location = col_ref->size() - 1;
            }
            vector<mirror> from_beg_to_end = trace_beam(mirrors_by_row[1]->front(), +1, 0);
            mirror last_mirror = from_beg_to_end.back();
            if (last_mirror.row == nrows && last_mirror.col == ncols + 1) {
                  return "0";
            }
            else if (nrows > 1) {
                  vector<mirror> from_end_to_beg = trace_beam(mirrors_by_row[nrows]->back(), +1, 0);
                  int lexminx = +oo, lexminy = +oo;
                  long long positions_count =
                        get_positions_count(make_segments(from_beg_to_end, 1), make_segments(from_end_to_beg, 0), lexminx, lexminy) +
                        get_positions_count(make_segments(from_end_to_beg, 1), make_segments(from_beg_to_end, 0), lexminx, lexminy);
                  if (positions_count > 0) {
                        ostringstream oss;
                        oss << positions_count << ' ' << lexminx << ' ' << lexminy;
                        return oss.str();
                  }
            }
            return "impossible";
      }
};

int main() {
      /**/
      freopen("input.txt", "r", stdin);
      freopen("output.txt", "w", stdout);
      /**/
      ios::sync_with_stdio(0);
      for (int test_case = 1; true; ++test_case) {
            int nrows, ncols, nmirrors[2];
            if (cin >> nrows >> ncols >> nmirrors[0] >> nmirrors[1]) {
                  vector<mirror> mirrors;
                  for (int type = 0; type < 2; ++type) {
                        for (int i = 0, row, col; i < nmirrors[type]; ++i) {
                              cin >> row >> col;
                              mirrors.push_back(mirror(row, col, 2 * type - 1));
                        }
                  }
                  cout << "Case " << test_case << ": " << (new processor(nrows, ncols))->process(mirrors) << endl;
            }
            else {
                  break;
            }
      }
      cout.flush();
      return 0;
}

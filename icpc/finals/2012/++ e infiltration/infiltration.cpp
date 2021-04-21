#include <cstdio>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>
using namespace std;

struct bit_set {
private:
      long long *_bits;
      int _segments_count;
public:
      bit_set(int total_bits_count) {
            _segments_count = (total_bits_count + 62) / 63;
            _bits = new long long[_segments_count];
            for (int i = 0; i < _segments_count; ++i) {
                  _bits[i] = 0;
            }
      }
      void include(int bit) {
            _bits[bit / 63] |= 1LL << (bit % 63);
      }
      void include(bit_set const *set) {
            for (int i = 0; i < _segments_count; ++i) {
                  _bits[i] |= set->_bits[i];
            }
      }
      void reset() {
            for (int i = 0; i < _segments_count; ++i) {
                  _bits[i] = 0;
            }
      }
      bool equals(bit_set const *set) {
            for (int i = 0; i < _segments_count; ++i) {
                  if (_bits[i] != set->_bits[i]) {
                        return false;
                  }
            }
            return true;
      }
};

istringstream* getLine() {
      static char buffer[256];
      cin.getline(buffer, 256);
      return new istringstream(buffer);
}

class dominating_set_algoritm {
protected:
      virtual vector<int> core_get_dominating_set(int n, vector<vector<int> > const &tournament) = 0;
public:
      vector<int> get_dominating_set(int n, vector<vector<int> > const &tournament) {
            return core_get_dominating_set(n, tournament);
      }
};

class bruteforce_dominating_set_algorithm : public dominating_set_algoritm {
private:
      int _threshold;
      vector<int> _dominating_set;
      bit_set *_model_set;
      bit_set *_cover_set;
      vector<bit_set*> _sets;
private:
      bool run(int n, int current, int cur_count, int max_count) {
            if (cur_count <= max_count) {
                  _cover_set->reset();
                  for (size_t i = 0; i < _dominating_set.size(); ++i) {
                        _cover_set->include(_sets[_dominating_set[i]]);
                  }
                  if (_cover_set->equals(_model_set)) {
                        return true;
                  }
                  if (cur_count < max_count) {
                        if (current < n) {
                              _dominating_set.push_back(current);
                              if (run(n, current + 1, cur_count + 1, max_count)) {
                                    return true;
                              }
                              _dominating_set.pop_back();
                              if (run(n, current + 1, cur_count + 0, max_count)) {
                                    return true;
                              }
                        }
                  }
            }
            return false;
      }
protected:
      virtual vector<int> core_get_dominating_set(int n, vector<vector<int> > const &tournament) {
            _model_set = new bit_set(n);
            _cover_set = new bit_set(n);
            for (int i = 0; i < n; ++i) {
                  _model_set->include(i);
                  _sets.push_back(new bit_set(n));
                  _sets[i]->include(i);
                  for (int j = 0; j < n; ++j) {
                        if (tournament[i][j]) {
                              _sets[i]->include(j);
                        }
                  }
            }
            for (int max_count = 1; max_count <= _threshold; ++max_count) {
                  if (run(n, 0, 0, max_count)) {
                        return _dominating_set;
                  }
            }
            return vector<int>();
      }
public:
      bruteforce_dominating_set_algorithm(int threshold) {
            _threshold = threshold;
      }
};

class greedy_dominating_set_algoritm : public dominating_set_algoritm {
protected:
      virtual vector<int> core_get_dominating_set(int n, vector<vector<int> > const &tournament) {
            vector<int> outdegree(n, 0);
            for (int i = 0; i < n; ++i) {
                  for (int j = 0; j < n; ++j) {
                        outdegree[i] += tournament[i][j];
                  }
            }
            vector<int> dominating_set;
            for (vector<int> covered(n, 0); true; ) {
                  int greedy = -1;
                  for (int i = 0; i < n; ++i) {
                        if (!covered[i]) {
                              if (greedy == -1 || outdegree[i] > outdegree[greedy]) {
                                    greedy = i;
                              }
                        }
                  }
                  if (greedy < 0) {
                        break;
                  }
                  dominating_set.push_back(greedy);
                  covered[greedy] = 1;
                  for (int i = 0; i < n; ++i) {
                        if (tournament[greedy][i]) {
                              if (!covered[i]) {
                                    covered[i] = 1;
                                    for (int j = 0; j < n; ++j) {
                                          if (!covered[j]) {
                                                if (tournament[j][i]) {
                                                      --outdegree[j];
                                                }
                                          }
                                    }
                              }
                        }
                  }
            }
            return dominating_set;
      }
};

int main() {
      /**/
      freopen("input.txt", "r", stdin);
      freopen("output.txt", "w", stdout);
      /**/

      for (int test_case = 1; true; ++test_case) {
            int n;
            if (*getLine() >> n) {
                  vector<vector<int> > tournament(n);
                  for (int i = 0; i < n; ++i) {
                        string descriptor;
                        *getLine() >> descriptor;
                        for (int j = 0; j < n; ++j) {
                              tournament[i].push_back(descriptor[j] == '1');
                        }
                  }
                  /** note: because underlying graph is a tournament it's always enough at most log(n) vertices
                   *  because the total sum of in-degree and out-degree is equal to the number of edges. */
                  vector<int> greedy_dominating_set = (new greedy_dominating_set_algoritm())->get_dominating_set(n, tournament);
                  vector<int> minimum_dominating_set =
                        (new bruteforce_dominating_set_algorithm(greedy_dominating_set.size() - 1))->
                              get_dominating_set(n, tournament);
                  if (minimum_dominating_set.size() < 1) {
                        minimum_dominating_set = greedy_dominating_set;
                  }
                  vector<int> covered(n, 0);
                  for (int i = 0; i < minimum_dominating_set.size(); ++i) {
                        int who = minimum_dominating_set[i];
                        covered[who] = 1;
                        for (int j = 0; j < n; ++j) {
                              if (tournament[who][j]) {
                                    covered[j] = 1;
                              }
                        }
                  }
                  int total_covered = 0;
                  for (int i = 0; i < n; ++i) {
                        total_covered += covered[i];
                  }
                  if (total_covered != n) {
                        minimum_dominating_set.clear();
                  }
                  cout << "Case " << test_case << ": " << minimum_dominating_set.size();
                  for (size_t i = 0; i < minimum_dominating_set.size(); ++i) {
                        cout << " " << minimum_dominating_set[i] + 1;
                  }
                  cout << endl;
            }
            else {
                  break;
            }
      }
      cout.flush();
      return 0; 
}

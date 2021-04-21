#include <cstdio>
#include <iostream>
#include <vector>
#include <string>
using namespace std;

char char2int(char c) {
      return ('A' <= c && c <= 'F' ? c - 'A' + 10 : c - '0');
}

char int2char(int i) {
      return (0 <= i && i <= 9 ? i + '0' : i - 10 + 'A');
}

int max(int a, int b) {
      return (a > b ? a : b);
}

vector<int> hex2bin(vector<int> const &hex_) {
      vector<int> bin_;
      for (size_t i = 0; i < hex_.size(); ++i) {
            int h = hex_[i];
            for (int i = 3; i >= 0; --i) {
                  bin_.push_back((h >> i) & 1);
            }
      }
      return bin_;
}

vector<int> bin2hex(vector<int> const &bin_) {
      vector<int> hex_;
      for (size_t i = 0,
                  k = (4 - bin_.size() % 4) % 4; i < bin_.size();) {
            int h = 0;
            for (int j = 0; j < 4; ++j) {
                  h = (h << 1) + bin_[i];
                  ++i, ++k;
                  if (k == 4) {
                        hex_.push_back(h);
                        k = 0;
                        break;
                  }
            }
      }
      return hex_;
}

void extract(vector<int> const &bin_, size_t &ix, vector<int> &quad) {
      if (bin_[ix] == 1) {
            quad.push_back(1);
            ++ix;
            quad.push_back(bin_[ix]);
            ++ix;
            return;
      }
      if (bin_[ix] == 0) {
            quad.push_back(0);
            ++ix;
            for (int j = 0; j < 4; ++j) {
                  extract(bin_, ix, quad);
            }
      }
}

void intersect(vector<int> const &bin0, size_t &i0, vector<int> const &bin1, size_t &i1, vector<int> &bin2) {
      if (bin0[i0] == 1 && bin1[i1] == 1) {
            bin2.push_back(1);
            ++i0, ++i1;
            bin2.push_back(bin0[i0] & bin1[i1]);
            ++i0, ++i1;
            return;
      }
      if (bin0[i0] == 1) {
            ++i0;
            if (bin0[i0] == 1) {
                  for (; i1 < bin1.size(); ++i1) {
                        bin2.push_back(bin1[i1]);
                  }
            }
            else {
                  bin2.push_back(1);
                  bin2.push_back(0);
            }
            return;
      }
      if (bin1[i1] == 1) {
            intersect(bin1, i1, bin0, i0, bin2);
            return;
      }
      if (1) {
            bin2.push_back(0);
            ++i0, ++i1;
            for (int j = 0; j < 4; ++j) {
                  vector<int> quad0, quad1;
                  extract(bin0, i0, quad0);
                  extract(bin1, i1, quad1);
                  size_t q0 = 0, q1 = 0;
                  intersect(quad0, q0, quad1, q1, bin2);
            }
      }
}

void compress(vector<int> const &bin_, size_t &ix, vector<int> &res) {
      if (bin_[ix] == 1) {
            res.push_back(1);
            ++ix;
            res.push_back(bin_[ix]);
            return;
      }
      if (1) {
            vector<int> quad[4];
            vector<int> res_[4];
            ++ix;
            int c = 0, p = 0;
            for (int j = 0; j < 4; ++j) {
                  size_t jx = 0;
                  extract(bin_, ix, quad[j]);
                  compress(quad[j], jx, res_[j]);
                  c += res_[j][0];
                  p += res_[j][1];
            }
            if (c == 4) {
                  if (p == 0 || p == 4) {
                        res.push_back(1);
                        res.push_back(p);
                        return;
                  }
            }
            res.push_back(0);
            for (int j = 0; j < 4; ++j) {
                  for (size_t k = 0; k < res_[j].size(); ++k) {
                        res.push_back(res_[j][k]);
                  }
            }
      }
}

string s;
vector<int> hex_[3];
vector<int> bin_[3];
vector<int> res;

int main() {
      for (int case_id = 1;; ++case_id) {
            for (int i = 0; i < 3; ++i) {
                  hex_[i].clear();
                  bin_[i].clear();
            }
            res.clear();
            for (int i = 0; i < 2; ++i) {
                  cin >> s;
                  for (size_t j = 0; j < s.length(); ++j) {
                        hex_[i].push_back(char2int(s[j]));
                  }
                  if (hex_[i][0] == 0) {
                        return 0;
                  }
                  bin_[i] = hex2bin(hex_[i]);
            }
            size_t i0 = 0, i1 = 0, i2 = 0;
            while (bin_[0][i0] == 0) {
                  ++i0;
            }
            while (bin_[1][i1] == 0) {
                  ++i1;
            }
            intersect(bin_[0], ++i0, bin_[1], ++i1, bin_[2]);
            compress(bin_[2], i2, res);
            res.insert(res.begin(), 1);
            hex_[2] = bin2hex(res);
            if (case_id > 1) {
                  cout << endl;
            }
            cout << "Image " << case_id << ":" << endl;
            for (size_t i = 0; i < hex_[2].size(); ++i) {
                  char c = int2char(hex_[2][i]);
                  cout << c;
            }
            cout << endl;
      }
      return 0;
}

#include <cstdio>
#include <memory>
#include <algorithm>
#include <vector>
#include <iostream>
using namespace std;

const int max_n = 64 + 1;
const int max_black_nodes_in_row = 12;
const int count_blocks = 4;

int image[max_n][max_n];
vector<int> codes;
int n, imageCounts = 0, c;

int pathToTen(int path) {
      int x = 0;
      for (int ten = 1; path > 0; ten *= 10, path /= 10)
            x = x * 10 + path % 10;
      for (int five = 1; x > 0; five *= 5, x /= 10)
            path += five * (x % 10);
      return path;
}

int tenToPath(int x) {
      int path = 0;
      for (; x > 0; x /= 5)
            path = path * 10 + x % 5;
      x = 0;
      for (; path > 0; path /= 10)
            x = x * 10 + path % 10;
      return x;
}

void buildCodes(int path, int u, int l, int d, int r) {
      int sum = image[d][r] - image[u - 1][r] - image[d][l - 1] + image[u - 1][l - 1];
      if (sum == 0 || sum == (d - u + 1) * (r - l + 1)) {
            if (sum != 0)
                  codes.push_back(pathToTen(path));
            return;
      }
      int x = (d - u) / 2 + 1;
      for (int i = 1; i <= count_blocks; ++ i)
            buildCodes(path * 10 + i, u + x * (i / 3), l + x * (1 - i % 2), d - x * (1 - i / 3), r - x * (i % 2));
}

void buildImage(int path, int u, int l, int d, int r) {
      if (path == 0) {
            for (int i = u; i <= d; ++ i)
                  for (int j = l; j <= r; ++ j)
                        image[i][j] = 1;
            return;
      }
      int i = path % 10, x = (d - u) / 2 + 1;
      buildImage(path / 10, u + x * (i / 3), l + x * (1 - i % 2), d - x * (1 - i / 3), r - x * (i % 2));
}

void process(int imageId) {
      if (imageId != 1)
            cout << endl;
      cout << "Image " << imageId << endl;
      if (n > 0) {
            buildCodes(0, 1, 1, n, n);
            if (!codes.empty()) {
                  sort(codes.begin(), codes.end());
                  for (int i = 1; i <= codes.size() - 1; ++i) {
                        cout << codes[i - 1];
                        if (i % max_black_nodes_in_row == 0)
                              cout << endl;
                        else
                              cout << " ";
                  }
                  cout << codes[codes.size() - 1] << endl;
            }
            cout << "Total number of black nodes = " << (codes.empty() ? 0 : codes.size()) << endl;
      }
      else {
            n = -n;
            for (int i = 0; i < codes.size(); ++ i)
                  buildImage(tenToPath(codes[i]), 1, 1, n, n);
            for (int i = 1; i <= n; ++ i) {
                  for (int j = 1; j <= n; ++ j)
                        if (image[i][j] == 0)
                              cout << ".";
                        else
                              cout << "*";
                  cout << endl;
            }
      }
}

int getPixel() {
      char c;
      while (true) {
            cin >> c;
            if (c == '0' || c == '1')
                  return (int)c - 48;
      }
}

int main() {
      cin >> n;
      while (n != 0) {
            for (int i = 0; i < max_n; ++i) {
                  for (int j = 0; j < max_n; ++j)
                        image[i][j] = 0;
            }
            codes.clear();
            if (n > 0)
                  for (int i = 1; i <= n; ++ i)
                        for (int j = 1; j <= n;) {
                              if ((c = getPixel()) == 0 || c == 1) {
                                    image[i][j] = image[i - 1][j] + image[i][j - 1] - image[i - 1][j - 1];
                                    image[i][j] += c;
                                    ++ j;
                              }
                        }
            else
                  while (true) {
                        cin >> c;
                        if (c == -1)
                              break;
                        codes.push_back(c);
                  }
                  process(++imageCounts);
                  cin >> n;
      }
      return 0;
}

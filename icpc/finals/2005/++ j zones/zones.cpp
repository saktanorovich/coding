#include <vector>
#include <cmath>
#include <cstdio>
#include <iostream>
using namespace std;

#define mask first
#define count second
#define customers first
#define locations second

const int max_towers = 20;
const int max_common_sets = 10;

int planned, toBuild, numCommonSets;
pair<int, vector<int> > result;
int customersServed[max_towers];
pair<int, int> commonSets[max_common_sets];

int ones(int x) {
      int result = 0;
      for (; x > 0; x -= x & (-x)) {
            result++;
      }
      return result;
}

void solve() {
      result.customers = 0;
      int bestSet = 0;
      for (int set = 0; set < 1 << planned; ++set) {
            int towersInSet = 0, customersInSet = 0;
            for (int j = 0; j < planned; ++j) {
                  if (((1 << j) & set) != 0) {
                        towersInSet++;
                        customersInSet += customersServed[j];
                  }
            }
            if (towersInSet == toBuild) {
                  for (int i = 0; i < numCommonSets; ++i) {
                        int temp = ones(commonSets[i].mask & set);
                        if (temp > 1) {
                              customersInSet -= (temp - 1) * commonSets[i].count;
                        }
                  }
                  if (customersInSet > result.customers) {
                        result.customers = customersInSet;
                        bestSet = set;
                  }
            }
      }
      for (int i = 0, j = 0; j < planned; ++j) {
            if (((1 << j) & bestSet) != 0) {
                  result.locations[i++] = j + 1;
            }
      }
}

int main() {
      result.second.assign(max_towers, 0);
      for (int i = 1; ; ++i) {
            cin >> planned >> toBuild;
            if (planned + toBuild == 0) {
                  return 0;
            }
            for (int j = 0; j < planned; ++j) {
                  cin >> customersServed[j];
            }
            int towerId, numTowers;
            cin >> numCommonSets;
            for (int j = 0; j < numCommonSets; ++j) {
                  cin >> numTowers;
                  commonSets[j].mask = 0;
                  for (int k = 0; k < numTowers; ++k) {
                        cin >> towerId;
                        commonSets[j].mask |= (1 << (towerId - 1));
                  }
                  cin >> commonSets[j].count;
            }
            solve();
            cout << "Case Number  " << i << endl;
            cout << "Number of Customers: " << result.customers << endl;
            cout << "Locations recommended:";
            for (int j = 0; j < toBuild; ++j) {
                  cout << " " << result.locations[j];
            }
            cout << endl << endl;
      }
      cout.flush();
}

#include <cmath>
#include <cstdio>
#include <iostream>
#include <vector>
using namespace std;

int const max_plate_diameter = 10000;
int const oo = +1000000000;

class processor {
private:
      vector<int> stacks_by_plate[max_plate_diameter + 1];
      vector<int> contains_stack[max_plate_diameter + 1];
      int splits_count[max_plate_diameter + 1];
public:
      int process(vector<vector<int> > const &stacks, int const &stacks_count) {
            for (int plate = 0; plate <= max_plate_diameter; ++plate) {
                  contains_stack[plate].assign(stacks_count, 0);
            }
            for (int i = 0; i < stacks_count; ++i) {
                  for (size_t j = 0; j < stacks[i].size(); ++j) {
                        if (j > 0 && stacks[i][j] == stacks[i][j - 1]) {
                              continue;
                        }
                        stacks_by_plate[stacks[i][j]].push_back(i);
                        contains_stack[stacks[i][j]][i] = 1;
                  }
            }
            for (int plate = 1; plate <= max_plate_diameter; ++plate) {
                  splits_count[plate] = 0;
                  for (size_t j = 0; j < stacks_by_plate[plate].size(); ++j) {
                        vector<int> stack = stacks[stacks_by_plate[plate][j]];
                        if (plate < stack[stack.size() - 1]) {
                              ++splits_count[plate];
                        }
                  }
            }
            vector<int> dp[max_plate_diameter + 1];
            dp[0].assign(stacks_count, 0);
            for (int plate = 1; plate <= max_plate_diameter; ++plate) {
                  if (stacks_by_plate[plate].size() == 0) {
                        dp[plate].assign(dp[plate - 1].begin(), dp[plate - 1].end());
                        contains_stack[plate].assign(contains_stack[plate - 1].begin(), contains_stack[plate - 1].end());
                        continue;
                  }
                  dp[plate].assign(stacks_count, +oo);
                  for (int prev_stack = 0; prev_stack < stacks_count; ++prev_stack) {
                        if (dp[plate - 1][prev_stack] < +oo) {
                              for (size_t i = 0; i < stacks_by_plate[plate].size(); ++i) {
                                    int curr_stack = stacks_by_plate[plate][i];
                                    int estimate = dp[plate - 1][prev_stack] + splits_count[plate];
                                    if (curr_stack != prev_stack || (stacks_by_plate[plate].size() == 1 && curr_stack == prev_stack)) {
                                          if (contains_stack[plate][prev_stack]) {
                                                estimate -= contains_stack[plate - 1][prev_stack];
                                          }
                                    }
                                    dp[plate][curr_stack] = min(dp[plate][curr_stack], estimate);
                              }
                        }
                  }
            }
            int splits = +oo;
            for (int i = 0; i < stacks_count; ++i) {
                  splits = min(splits, dp[max_plate_diameter][i]);
            }
            return 2 * splits + stacks_count - 1;
      }
};

int main() {
      for (int test_case = 1; true; ++test_case) {
            int stacks_count;
            vector<vector<int> > stacks;
            if (cin >> stacks_count) {
                  for (int i = 0, plates_count, plate; i < stacks_count; ++i) {
                        vector<int> stack;
                        cin >> plates_count;
                        for (int j = 0; j < plates_count; ++j) {
                              cin >> plate;
                              stack.push_back(plate);
                        }
                        stacks.push_back(stack);
                  }
                  cout << "Case " << test_case << ": " << (new processor())->process(stacks, stacks_count) << endl;
            }
            else {
                  break;
            }
      }
      cout.flush();
      return 0;
}

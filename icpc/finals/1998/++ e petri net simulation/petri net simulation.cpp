#include <algorithm>
#include <cassert>
#include <cstdio>
#include <string>
#include <vector>
using namespace std;

int const max_number_of_places = 100;
int const max_number_of_transitions = 100;

int number_of_places, number_of_transitions, number_of_firings;
int number_of_tokens[max_number_of_places];
vector<int> in_places[max_number_of_transitions + 1];
vector<int> ou_places[max_number_of_transitions + 1];
int order[max_number_of_places + 1];

int is_transition_enabled(int transition_indx) {
      int tokens_count[max_number_of_places];
      for (int i = 1; i <= number_of_places; ++i) {
            tokens_count[i] = number_of_tokens[i];
      }
      for (int i = 0; i < in_places[transition_indx].size(); ++i) {
            if (tokens_count[in_places[transition_indx][i]] < 1) {
                  return 0;
            }
            --tokens_count[in_places[transition_indx][i]];
      }
      return 1;
}

int get_any_enabled_transition() {
      for (int t = 1; t <= number_of_transitions; ++t) {
            if (is_transition_enabled(t)) {
                  return t;
            }
      }
      return 0;
}

int main() {
      for (int test_case = 1; true; ++test_case) {
            scanf("%d", &number_of_places);
            if (number_of_places == 0) {
                  break;
            }
            for (int i = 1; i <= number_of_places; ++i) {
                  scanf("%d", &number_of_tokens[i]);
            }
            scanf("%d", &number_of_transitions);
            for (int i = 1; i <= number_of_transitions; ++i) {
                  in_places[i].clear();
                  ou_places[i].clear();
            }
            for (int i = 1; i <= number_of_transitions; ++i) {
                  for (int place_indx; true;) {
                        scanf("%d", &place_indx);
                        if (place_indx == 0) {
                              break;
                        }
                        if (place_indx < 0) {
                              in_places[i].push_back(-place_indx);
                        }
                        else {
                              ou_places[i].push_back(+place_indx);
                        }
                  }
            }
            scanf("%d", &number_of_firings);

            printf("Case %d: ", test_case);
            int number_of_competed_firings = 0;
            for (int f = 1; f <= number_of_firings; ++f) {
                  int any_transition_indx = get_any_enabled_transition();
                  if (!any_transition_indx) {
                        break;
                  }
                  vector<int> ins = in_places[any_transition_indx];
                  vector<int> ous = ou_places[any_transition_indx];
                  for (int i = 0; i < ins.size(); ++i) {
                        --number_of_tokens[ins[i]];
                  }
                  for (int i = 0; i < ous.size(); ++i) {
                        ++number_of_tokens[ous[i]];
                  }
                  ++number_of_competed_firings;
            }
            string format = "still live after %d transitions\n";
            if (number_of_competed_firings < number_of_firings) {
                  format = "dead after %d transitions\n";
            }
            printf(format.c_str(), number_of_competed_firings);
            printf("Places with tokens:");
            for (int i = 1; i <= number_of_places; ++i) {
                  if (number_of_tokens[i] > 0) {
                        printf(" %d (%d)", i, number_of_tokens[i]);
                  }
            }
            printf("\n\n");
      }
      return 0;
}

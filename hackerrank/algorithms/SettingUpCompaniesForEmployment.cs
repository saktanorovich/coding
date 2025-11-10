using System;

namespace interview.hackerrank {
    public class SettingUpCompaniesForEmployment {
        public int evaluate(int[] population, int numOfCompanies) {
            int lo = 1, hi = (int)5e6 + 1;
            while (lo < hi) {
                var target = (lo + hi) >> 1;
                if (possible(population, numOfCompanies, target)) {
                    hi = target;
                }
                else {
                    lo = target + 1;
                }
            }
            return lo;
        }

        private bool possible(int[] population, int numOfCompanies, int target) {
            var required = 0;
            foreach (var people in population) {
                required += (people + target - 1) / target;
            }
            if (required <= numOfCompanies) {
                return true;
            }
            return false;
        }
    }
}

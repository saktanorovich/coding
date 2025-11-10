using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class PolicePair {
        public int[] bestSquad(int[] skillStart, int[] skillEnd) {
            var skills = new int[maxSkill + 1];
            for (var i = 0; i < skillStart.Length; ++i)
                for (var skill = skillStart[i]; skill <= skillEnd[i]; ++skill) {
                    ++skills[skill];
                }
            return bestSquad(skills.Sum(), skills);
        }

        private static int[] bestSquad(int numOfOfficers, int[] skills) {
            int left = numOfOfficers, best = 0;
            for (var squad = 0; squad <= 2 * maxSkill; ++squad) {
                var assigned = assign((int[])skills.Clone(), squad);
                if (left > numOfOfficers - assigned) {
                    left = numOfOfficers - assigned;
                    best = squad;
                }
                else if (left == numOfOfficers - assigned) {
                    best = squad;
                }
            }
            return new[] { left, best / 2 };
        }

        private static int assign(int[] skills, int squad) {
            var result = 0;
            for (var skill = 0; skill <= maxSkill; ++skill) {
                if (skill <= squad && squad - skill <= maxSkill)
                    while (skills[skill] > 0) {
                        if (skills[squad - skill] > 0) {
                            if (skill != squad - skill || skills[skill] > 1) {
                                result += 2;
                                --skills[+skill];
                                --skills[-skill + squad];
                                continue;
                            }
                        }
                        break;
                    }
            }
            return result;
        }

        private const int maxSkill = 1000;
    }
}
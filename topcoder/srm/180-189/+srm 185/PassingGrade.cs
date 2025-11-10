using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class PassingGrade {
        public int pointsNeeded(int[] pointsEarned, int[] pointsPossible, int finalExam) {
            var earned = pointsEarned.Sum();
            var xtotal = pointsPossible.Sum() + finalExam;
            for (var need = 0; need <= finalExam; ++need) {
                if ((earned + need) * 100 >= 65 * xtotal) {
                    return need;
                }
            }
            return -1;
        }
    }
}

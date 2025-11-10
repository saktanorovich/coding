using System;
using System.Text;

namespace TopCoder.Algorithm {
    public class TrueFalse {
        public string answerKey(string[] graded) {
            var points = new int[graded.Length];
            var answer = new string[graded.Length];
            for (var i = 0; i < graded.Length; ++i) {
                var data = graded[i].Split(' ');
                points[i] = int.Parse(data[0]);
                answer[i] = data[1];
            }
            return answerKey(points, answer, answer[0].Length);
        }

        private static string answerKey(int[] points, string[] answer, int n) {
            for (var mask = 0; mask < 1 << n; ++mask) {
                var test = buildTest(mask, n);
                var answered = 0;
                for (var i = 0; i < answer.Length; ++i) {
                    var correct = 0;
                    for (var q = 0; q < n; ++q) {
                        if (answer[i][q] == test[q]) {
                            ++correct;
                            answered |= 1 << q;
                        }
                    }
                    if (points[i] != correct) goto next;
                }
                if (answered + 1 == 1 << n)
                    return test;
                next: ;
            }
            return "inconsistent";
        }

        private static string buildTest(int mask, int n) {
            var result = new StringBuilder();
            for (var i = 0; i < n; ++i) {
                result.Append("FT"[(mask >> (n - 1 - i) & 1)]);
            }
            return result.ToString();
        }
    }
}
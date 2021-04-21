using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class NoOrderOfOperations {
        public int evaluate(string expr) {
            var result = expr[0] - '0';
            for (var i = 1; i < expr.Length; ++i) {
                switch (expr[i]) {
                    case '+': result += expr[i + 1] - '0'; break;
                    case '-': result -= expr[i + 1] - '0'; break;
                    case '*': result *= expr[i + 1] - '0'; break;
                }
            }
            return result;
        }
    }
}
using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class SimpleCalculator {
        public int calculate(string input) {
            var data = input.Split(new[] { '+', '*', '-', '/' });
            var num1 = int.Parse(data[0]);
            var num2 = int.Parse(data[1]);
            if (input.IndexOf("+") >= 0) return num1 + num2;
            if (input.IndexOf("*") >= 0) return num1 * num2;
            if (input.IndexOf("-") >= 0) return num1 - num2;
            if (input.IndexOf("/") >= 0) return num1 / num2;
            return 0;
        }
    }
}
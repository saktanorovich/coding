using System;

namespace interview.hackerrank {
    public class SuperStack {
        private readonly long[] top = new long[1000000 + 1];
        private readonly long[] inc = new long[1000000 + 1];

        public string[] process(int numOfOperations, string[] operations) {
            var result = new string[numOfOperations];
            for (int version = 0, op = 0; op < numOfOperations; ++op) {
                var tokens = operations[op].Split(' ');
                switch (tokens[0]) {
                    case "push":
                        version = version + 1;
                        top[version] = int.Parse(tokens[1]);
                        inc[version] = 0;
                        break;
                    case "inc":
                        inc[int.Parse(tokens[1])] += int.Parse(tokens[2]);
                        break;
                    case "pop":
                        inc[version - 1] += inc[version];
                        version = version - 1;
                        break;
                }
                var peekTop = "EMPTY";
                if (version > 0) {
                    peekTop = (top[version] + inc[version]).ToString();
                }
                result[op] = peekTop;
            }
            return result;
        }
    }
}

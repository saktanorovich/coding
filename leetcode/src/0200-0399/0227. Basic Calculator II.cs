using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0227 {
        public int Calculate(string s) {
            return new Calculator().eval(new Expression(new Tokenizer(s)));
        }

        private class Expression {
            private readonly Tokenizer tokenizer;
            private readonly Stack<Token> funcs;
            private readonly List<Token> terms;
            private Token last;

            public Expression(Tokenizer tokenizer) {
                this.tokenizer = tokenizer;
                this.funcs = new Stack<Token>();
                this.terms = new List<Token>();
            }

            public IEnumerable<Token> stream() {
                last = new Token("", 2);
                while (tokenizer.hasTokens()) {
                    var token = tokenizer.nextToken();
                    if (token.type == 0) {
                        num(token);
                    } else {
                        fun(token);
                    }
                    last = token;
                }
                while (funcs.Count > 0) {
                    terms.Add(funcs.Pop());
                }
                return terms;
            }

            private void num(Token token) {
                terms.Add(new Token(token.value, 0));
            }

            private void fun(Token token) {
                Token term;
                if (last.type == 0) {
                    term = new Token(token.value, 2);
                } else {
                    term = new Token(token.value, 1);
                }
                while (funcs.Count > 0) {
                    if (pri(funcs.Peek()) >= pri(term)) {
                        terms.Add(funcs.Pop());
                    } else {
                        break;
                    }
                }
                funcs.Push(term);
            }

            private int pri(Token term) {
                if (term.type == 1) {
                    return 2;
                }
                switch (term.value) {
                    case "+":
                    case "-":
                        return 0;
                    case "*":
                    case "/":
                        return 1;
                }
                throw new InvalidOperationException();
            }
        }

        private class Calculator {
            private readonly Dictionary<string, Func<int, int, int>> binary;
            private readonly Dictionary<string, Func<int, int>> unary;

            public Calculator() {
                binary = new Dictionary<string, Func<int, int, int>> {
                    ["+"] = (x, y) => x + y,
                    ["-"] = (x, y) => x - y,
                    ["*"] = (x, y) => x * y,
                    ["/"] = (x, y) => x / y,
                };
                unary = new Dictionary<string, Func<int, int>> {
                    ["+"] = x => +x,
                    ["-"] = x => -x
                };
            }

            public int eval(Expression expr) {
                var nums = new Stack<int>();
                foreach (var term in expr.stream()) {
                    if (term.type == 0) {
                        nums.Push(int.Parse(term.value));
                    }
                    else if (term.type == 1) {
                        var x = nums.Pop();
                        var z = unary[term.value](x);
                        nums.Push(z);
                    }
                    else if (term.type == 2) {
                        var y = nums.Pop();
                        var x = nums.Pop();
                        var z = binary[term.value](x, y);
                        nums.Push(z);
                    }
                }
                if (nums.Count > 0) {
                    return nums.Single();
                } else {
                    return 0;
                }
            }
        }

        private class Token {
            public readonly string value;
            public readonly int type;

            public Token(string value, int type) {
                this.value = value;
                this.type = type;
            }
        }

        private class Tokenizer {
            private readonly string stream;
            private int cursor;

            public Tokenizer(string stream) {
                this.stream = stream;
                this.cursor = 0;
            }

            public bool hasTokens() {
                while (cursor < stream.Length) {
                    if (Char.IsWhiteSpace(stream[cursor])) {
                        cursor++;
                    } else {
                        break;
                    }
                }
                return cursor < stream.Length;
            }

            public Token nextToken() {
                var value = new StringBuilder();
                if ('0' <= stream[cursor] && stream[cursor] <= '9') {
                    scan(value, x => '0' <= x && x <= '9');
                    return new Token(value.ToString(), 0);
                }
                var c = stream[cursor++];
                value.Append(c);
                return new Token(value.ToString(), 1);
            }

            private void scan(StringBuilder value, Predicate<char> condition) {
                while (cursor < stream.Length) {
                    var c = stream[cursor];
                    if (condition(c)) {
                        value.Append(stream[cursor++]);
                    } else {
                        break;
                    }
                }
            }
        }
    }
}

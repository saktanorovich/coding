using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0736 {
        public int Evaluate(string expression) {
            return new Processor(new Tokenizer(expression)).evaluate();
        }

        private class Processor {
            private readonly Tokenizer tokenizer;
            private readonly Stack<Dictionary<string, int>> scope;

            public Processor(Tokenizer tokenizer) {
                this.tokenizer = tokenizer;
                this.scope = new Stack<Dictionary<string, int>>();
                this.scope.Push(new Dictionary<string, int>());
            }

            public int evaluate() {
                var token = next();
                switch (token.type) {
                    case 0: return operation();
                    case 5: return parse(token);
                    case 6: return get(token);
                }
                throw new InvalidOperationException();
            }

            private int operation() {
                var token = next();
                switch (token.type) {
                    case 2: return mul();
                    case 3: return add();
                    case 4: return let();
                }
                throw new InvalidOperationException();
            }

            private int mul() {
                var res = evaluate() * evaluate();
                next();
                return res;
            }

            private int add() {
                var res = evaluate() + evaluate();
                next();
                return res;
            }

            private int let() {
                scope.Push(new Dictionary<string, int>(
                    scope.Peek()
                ));
                var eval = 0;
                var last = new Token("let", 4);
                for (var token = next(); token.type != 1;) {
                    switch (token.type) {
                        case 0: eval = operation();  break;
                        case 5: eval = parse(token); break;
                    }
                    if (last.type == 6)
                        set(last, eval);
                    last = token;
                    token = next();
                }
                eval = last.type == 6 ? get(last) : eval;
                scope.Pop();
                return eval;
            }

            private int get(Token var) {
                return scope.Peek()[var.value];
            }

            private void set(Token var, int val) {
                scope.Peek()[var.value] = val;
            }

            private int parse(Token token) {
                return int.Parse(token.value);
            }

            private Token next() {
                if (tokenizer.hasTokens()) {
                    return tokenizer.nextToken();
                }
                throw new InvalidOperationException();
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
                scan(new StringBuilder(), x => x == ' ');
                return cursor < stream.Length;
            }

            public Token nextToken() {
                var c = stream[cursor++];
                if (c == '(') return new Token(c.ToString(), 0);
                if (c == ')') return new Token(c.ToString(), 1);
                var buffer = new StringBuilder();
                buffer.Append(c);
                scan(buffer, x => x != ' ' && x != '(' && x != ')');
                var value = buffer.ToString();
                if (value == "mult") return new Token(value, 2);
                if (value == "add" ) return new Token(value, 3);
                if (value == "let" ) return new Token(value, 4);
                if (int.TryParse(value.ToString(), out _))
                    return new Token(value, 5);
                else
                    return new Token(value, 6);
            }

            private void scan(StringBuilder buffer, Predicate<char> condition) {
                while (cursor < stream.Length) {
                    var c = stream[cursor];
                    if (condition(c)) {
                        buffer.Append(stream[cursor++]);
                    } else {
                        break;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0726 {
        public string CountOfAtoms(string formula) {
            var processor = new Processor();
            var tokenizer = new Tokenizer(formula);
            while (tokenizer.hasTokens()) {
                var token = tokenizer.nextToken();
                processor.handle(token);
            }
            var res = new StringBuilder();
            foreach (var e in processor.result()) {
                res.Append(e.Key);
                if (e.Value > 1) {
                    res.Append(e.Value);
                }
            }
            return res.ToString();
        }

        private class Processor {
            private Stack<IDictionary<string, int>> stack;
            private Token lastToken;

            public Processor() {
                stack = new Stack<IDictionary<string, int>>();
                stack.Push(new SortedDictionary<string, int>());
                lastToken = null;
            }

            public void handle(Token token) {
                switch (token.type) {
                    case 0: AtomName(token); break;
                    case 1: AtomsCnt(token); break;
                    case 2: OBracket(token); break;
                    case 3: CBracket(token); break;
                }
                lastToken = token;
            }

            public IDictionary<string, int> result() {
                handle(new Token("1", 1));
                return stack.Single();
            }

            private void AtomName(Token token) {
                if (lastToken?.type == 0) {
                    add(lastToken.value, 1);
                }
                if (lastToken?.type == 3) {
                    add(stack.Pop());
                }
            }

            private void AtomsCnt(Token token) {
                var num = int.Parse(token.value);
                if (lastToken.type == 0) {
                    add(lastToken.value, num);
                }
                if (lastToken.type == 3) {
                    mul(stack.Peek(), num);
                    add(stack.Pop());
                }
            }

            private void OBracket(Token token) {
                if (lastToken?.type == 0) {
                    add(lastToken.value, 1);
                }
                if (lastToken?.type == 3) {
                    add(stack.Pop());
                }
                stack.Push(new Dictionary<string, int>());
            }

            private void CBracket(Token token) {
                if (lastToken?.type == 0) {
                    add(lastToken.value, 1);
                }
                if (lastToken?.type == 3) {
                    add(stack.Pop());
                }
            }

            private void mul(IDictionary<string, int> atoms, int num) {
                foreach (var atom in atoms.Keys.ToArray()) {
                    atoms[atom] *= num;
                }
            }

            private void add(IDictionary<string, int> atoms) {
                foreach (var e in atoms) {
                    add(e.Key, e.Value);
                }
            }

            private void add(string atom, int count) {
                var map = stack.Peek();
                if (map.ContainsKey(atom) == false) {
                    map.Add(atom, count);
                } else {
                    map[atom] += count;
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
                return cursor < stream.Length;
            }

            public Token nextToken() {
                var value = new StringBuilder();
                if ('A' <= stream[cursor] && stream[cursor] <= 'Z') {
                    value.Append(stream[cursor++]);
                    scan(value, x => 'a' <= x && x <= 'z');
                    return new Token(value.ToString(), 0);
                }
                if ('0' <= stream[cursor] && stream[cursor] <= '9') {
                    scan(value, x => '0' <= x && x <= '9');
                    return new Token(value.ToString(), 1);
                }
                var c = stream[cursor++];
                value.Append(c);
                if (c == '(') {
                    return new Token(value.ToString(), 2);
                } else {
                    return new Token(value.ToString(), 3);
                }
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

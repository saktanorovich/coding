using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_0770 {
        public IList<string> BasicCalculatorIV(string expression, string[] evalvars, int[] evalints) {
            var eval = new Dictionary<string, int>();
            for (var i = 0; i < evalvars.Length; ++i) {
                eval[evalvars[i]] = evalints[i];
            }
            var postfix = new ShuntingYard(new Tokenizer(expression)).ToPostfixForm();
            var stack = new Stack<Polynomial>();
            foreach (var token in postfix) {
                switch (token.type) {
                    case 2: { var b = stack.Pop(); var a = stack.Pop(); stack.Push(a.Mul(b)); break; }
                    case 3: { var b = stack.Pop(); var a = stack.Pop(); stack.Push(a.Add(b)); break; }
                    case 4: { var b = stack.Pop(); var a = stack.Pop(); stack.Push(a.Sub(b)); break; }
                    case 5:
                        stack.Push(new Polynomial(int.Parse(token.value)));
                        break;
                    case 6:
                        if (eval.TryGetValue(token.value, out var value)) {
                            stack.Push(new Polynomial(value));
                        } else {
                            stack.Push(new Polynomial(token.value));
                        }
                        break;
                }
            }
            return stack.Single().AsTerms();
        }

        private sealed class Term : IReadOnlyList<string>, IComparable<Term> {
            private readonly List<string> items;
            private int hash;

            public Term(int capacity) {
                items = new List<string>(capacity);
                hash = 0;
            }

            public string this[int index] => items[index];

            public int Count => items.Count;

            public IEnumerator<string> GetEnumerator() {
                return items.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            public void Add(string item) {
                items.Add(item);
                hash ^= item.GetHashCode();
            }

            public void Add(Term term) {
                items.AddRange(term.items);
                hash ^= term.hash;
            }

            public void Sort() {
                items.Sort();
            }

            public override int GetHashCode() {
                return hash;
            }

            public override bool Equals(object other) {
                var term = (Term)other;
                if (hash != term.hash) {
                    return false;
                }
                var count = items.Count;
                if (count != term.items.Count) {
                    return false;
                }
                for (var i = 0; i < count; ++i) {
                    if (items[i].Equals(term.items[i]) == false) {
                        return false;
                    }
                }
                return true;
            }

            public int CompareTo(Term t) {
                var count = items.Count;
                if (count == t.items.Count) {
                    for (var i = 0; i < count; ++i) {
                        var sign = items[i].CompareTo(t.items[i]);
                        if (sign != 0) {
                            return sign;
                        }
                    }
                }
                return t.items.Count - count;
            }
        }

        private class Polynomial {
            private readonly Dictionary<Term, int> terms;

            private Polynomial() {
                terms = new Dictionary<Term, int>();
            }

            public Polynomial(string term) : this() {
                terms.Add(new Term(1) { term }, 1);
            }

            public Polynomial(int value) : this() {
                terms.Add(new Term(0), value);
            }

            public Polynomial Mul(Polynomial that) {
                var res = new Polynomial();
                foreach (var term1 in this.terms) {
                    foreach (var term2 in that.terms) {
                        var term = new Term(term1.Key.Count + term2.Key.Count);
                        term.Add(term1.Key);
                        term.Add(term2.Key);
                        term.Sort();
                        res.Put(term, term1.Value * term2.Value);
                    }
                }
                return res;
            }

            public Polynomial Add(Polynomial that) {
                var res = new Polynomial();
                foreach (var term in this.terms) res.Put(term.Key, +term.Value);
                foreach (var term in that.terms) res.Put(term.Key, +term.Value);
                return res;
            }

            public Polynomial Sub(Polynomial that) {
                var res = new Polynomial();
                foreach (var term in this.terms) res.Put(term.Key, +term.Value);
                foreach (var term in that.terms) res.Put(term.Key, -term.Value);
                return res;
            }

            private void Put(Term term, int value) {
                terms.TryAdd(term, 0);
                terms[term] += value;
            }

            public IList<string> AsTerms() {
                var res = new List<string>();
                foreach (var term in terms.OrderBy(t => t.Key)) {
                    if (term.Value != 0) {
                        if (term.Key.Count > 0) {
                            res.Add($"{term.Value}*{String.Join('*', term.Key)}");
                        } else {
                            res.Add($"{term.Value}");
                        }
                    }
                }
                return res;
            }
        }

        private class ShuntingYard {
            private readonly Tokenizer tokenizer;
            private readonly Stack<Token> stack;
            private List<Token> queue;

            public ShuntingYard(Tokenizer tokenizer) {
                this.tokenizer = tokenizer;
                this.stack = new Stack<Token>();
            }

            public IEnumerable<Token> ToPostfixForm() {
                queue = new List<Token>();
                while (tokenizer.hasTokens()) {
                    var token = tokenizer.nextToken();
                    switch (token.type) {
                        case 0: OBracket(token); break;
                        case 1: CBracket(token); break;
                        case 2:
                        case 3:
                        case 4: Operator(token); break;
                        case 5:
                        case 6: Variable(token); break;
                    }
                }
                while (stack.Count > 0) {
                    var token = stack.Pop();
                    queue.Add(token);
                }
                return queue;
            }

            private void OBracket(Token token) {
                stack.Push(token);
            }

            private void CBracket(Token token) {
                while (true) {
                    token = stack.Pop();
                    if (token.type == 0) {
                        break;
                    }
                    queue.Add(token);
                }
            }

            private void Variable(Token token) {
                queue.Add(token);
            }

            private void Operator(Token token) {
                while (stack.Count > 0) {
                    var top = stack.Peek();
                    if (Priority(top) >= Priority(token)) {
                        stack.Pop();
                        queue.Add(top);
                    }
                    else break;
                }
                stack.Push(token);
            }

            private int Priority(Token token) {
                switch (token.value) {
                    case "*": return 2;
                    case "+": return 1;
                    case "-": return 1;
                }
                return 0;
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
                if (c == '*') return new Token(c.ToString(), 2);
                if (c == '+') return new Token(c.ToString(), 3);
                if (c == '-') return new Token(c.ToString(), 4);
                var buffer = new StringBuilder();
                buffer.Append(c);
                scan(buffer, x => x != ' ' && x != '(' && x != ')' && x != '*' && x != '+' && x != '-');
                var value = buffer.ToString();
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

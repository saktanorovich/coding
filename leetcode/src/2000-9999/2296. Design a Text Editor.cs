using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class Solution_2296 {
        public class TextEditor {
            private readonly Stack<char> lt;
            private readonly Stack<char> rt;

            public TextEditor() {
                lt = new Stack<char>();
                rt = new Stack<char>();
            }

            public void AddText(string text) {
                foreach (var c in text) {
                    lt.Push(c);
                }
            }

            public int DeleteText(int k) {
                var d = 0;
                while (k-- > 0 && lt.Count > 0) {
                    lt.Pop();
                    d = d + 1;
                }
                return d;
            }

            public string CursorLeft(int k) {
                while (k-- > 0 && lt.Count > 0) {
                    rt.Push(lt.Pop());
                }
                return LastTen();
            }

            public string CursorRight(int k) {
                while (k-- > 0 && rt.Count > 0) {
                    lt.Push(rt.Pop());
                }
                return LastTen();
            }

            private string LastTen() {
                var ten = new StringBuilder();
                for (var i = 0; i < 10; ++i) {
                    if (lt.Count > 0) {
                        ten.Insert(0, lt.Pop());
                    }
                    else break;
                }
                foreach (var c in ten.ToString()) {
                    lt.Push(c);
                }
                return ten.ToString();
            }
        }
    }
}
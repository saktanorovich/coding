using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coding.leetcode {
    public class BrowserHistory {
        private string[] h;
        private int p;
        private int t;
  
        public BrowserHistory(string homepage) {
            h = new string[128];
            h[0] = homepage;
        }
    
        public void Visit(string url) {
            h[++p] = url;
            t = p;
        }

        public string Back(int steps) {
            p = Math.Max(0, p - steps);
            return h[p];
        }

        public string Forward(int steps) {
            p = Math.Min(t, p + steps);
            return h[p];
        }
    }
/*
    public class BrowserHistory {
        private readonly Stack<string> b;
        private readonly Stack<string> f;
  
        public BrowserHistory(string homepage) {
            b = new Stack<string>();
            f = new Stack<string>();
            b.Push(homepage);
        }
    
        public void Visit(string url) {
            b.Push(url);
            f.Clear();
        }

        public string Back(int steps) {
            while (steps > 0 && b.Count > 1) {
                f.Push(b.Pop());
                steps--;
            }
            return b.Peek();
        }

        public string Forward(int steps) {
            while (steps > 0 && f.Count > 0) {
                b.Push(f.Pop());
                steps--;
            }
            return b.Peek();
        }
/**/
}

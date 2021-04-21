using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class WindowManager {
        public string[] screen(int H, int W, string[] windows) {
            var buffer = new char[H][];
            for (var x = 0; x < H; ++x) {
                buffer[x] = new char[W];
                for (var y = 0; y < W; ++y) {
                    buffer[x][y] = ' ';
                }
            }
            foreach (var window in windows) {
                var data = window.Split(' ');
                var th = int.Parse(data[0]);
                var tw = int.Parse(data[1]);
                var vh = int.Parse(data[2]);
                var vw = int.Parse(data[3]);
                var fi = data[4][0];
                var x0 = th;
                var y0 = tw;
                var x1 = th + vh - 1;
                var y1 = tw + vw - 1;
                clamp(0, H - 1, ref x0, ref x1);
                clamp(0, W - 1, ref y0, ref y1);
                for (var x = x0; x <= x1; ++x) {
                    for (var y = y0; y <= y1; ++y) {
                        buffer[x][y] = getChar(th, tw, th + vh - 1, tw + vw - 1, x, y, fi);
                    }
                }
                
            }
            var result = new string[H];
            for (var h = 0; h < H; ++h) {
                result[h] = new string(buffer[h]);
            }
            return result;
        }

        private static char getChar(int x0, int y0, int x1, int y1, int x, int y, char c) {
            if (x == x0 || x == x1) if (y0 < y && y < y1) return '-'; else return '+';
            if (y == y0 || y == y1) if (x0 < x && x < x1) return '|'; else return '+';
            return c;
        }

        private static void clamp(int x0, int x1, ref int a, ref int b) {
            if (b < x0 || x1 < a) {
                a = +1;
                b = -1;
            }
            else {
                a = Math.Max(a, x0);
                b = Math.Min(b, x1);
            }
        }
    }
}
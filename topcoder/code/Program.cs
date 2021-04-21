using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    internal class Program {
        private static string ToString<T>(params T[] a) {
            var result = string.Empty;
            for (var i = 0; i < a.Length; ++i) {
                result += a[i].ToString();
                if (i + 1 < a.Length) {
                    result += " ";//Environment.NewLine;
                }
            }
            return result;
        }

        internal static void Main(string[] args) {
            var c = new ShadowArea();
            Console.WriteLine(c.area(new string[] {
                ".*#...",
                "......",
                ".#...#",
                ".....#",
                ".....#"
            }));
            Console.WriteLine(c.area(new string[] {
                "..............................",
                "..............................",
                "..........#...................",
                ".........#*#..................",
                "..........#...................",
                "..............................",
                "..............................",
                "..............................",
                "..............................",
                ".............................."
            }));
            Console.WriteLine(c.area(new string[] {
                ".#....*",
                "##.....",
                "#......"
            }));
            Console.WriteLine(c.area(new string[] {
                "..........",
                "..........",
                "..........",
                "###..#####",
                "..........",
                "*........."
            }));
            Console.WriteLine(c.area(new string[] {
                "...........",
                "...........",
                "......#....",
                "........#..",
                "..........#",
                "..........*"
            }));
            Console.WriteLine(c.area(new string[] {
                "*"
            }));
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
        }
    }
}

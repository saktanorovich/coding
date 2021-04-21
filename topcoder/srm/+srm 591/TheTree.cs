using System;

      public class TheTree {
            public int maximumDiameter(int[] cnt) {
                  int d = cnt.Length;
                  for (int k = 0; k < d; ++k) {
                        if (cnt[k] == 1) {
                              return Math.Max(k + d, maximumDiameter(subArray(cnt, k + 1)));
                        }
                  }
                  return 2 * d;
            }

            private int[] subArray(int[] a, int startIndex) {
                  int[] result = new int[a.Length - startIndex];
                  for (int i = startIndex; i < a.Length; ++i) {
                        result[i - startIndex] = a[i];
                  }
                  return result;
            }

            internal static void Main(string[] args) {
                  Console.WriteLine(new TheTree().maximumDiameter(new int[] {3})); // 2
                  Console.WriteLine(new TheTree().maximumDiameter(new int[] {2, 2})); // 4
                  Console.WriteLine(new TheTree().maximumDiameter(new int[] {4, 1, 2, 4})); // 5
                  Console.WriteLine(new TheTree().maximumDiameter(new int[] {
                        4, 2, 1, 3, 2, 5, 7, 2, 4, 5, 2, 3, 1, 13, 6})); // 21

                  Console.WriteLine("Press any key...");
                  Console.ReadKey();
            }
      }

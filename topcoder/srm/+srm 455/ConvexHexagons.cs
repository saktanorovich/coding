using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class ConvexHexagons {
            private const long modulo = 1000000007;

            public int find(int n) {
                  long[] f = new long[n + 1];
                  long[] g = new long[n + 1]; /* the number of hexagons with three sides lying on the sides of the triangle. */
                  if (n > 2) {
                        f[3] = 1;
                        g[3] = 1;
                        for (long i = 4; i <= n; ++i) {
                              /* the hexagon is uniquely defined by three integers a,b,c such that a+b<n, a+c<n, b+c<n. These
                               * integers denote the sides of the triangles which touch interior of the hexagon. */
                              g[i] = ((3 * (i - 1) * (i - 2) / 2 - 3 * (i - 3) - 2) % modulo + g[i - 2]) % modulo;

                              /* use inclusion-exclusion principle for interior triangles. */
                              f[i] = ((3 * f[i - 1] - 3 * f[i - 2] + f[i - 3] + 3 * modulo) % modulo + g[i]) % modulo;
                        }
                  }
                  return (int)f[n];
            }

            public static void Main() {
                  Console.WriteLine(new ConvexHexagons().find(4)); // 7
                  Console.WriteLine(new ConvexHexagons().find(5)); // 29
                  Console.WriteLine(new ConvexHexagons().find(6)); // 90
                  Console.WriteLine(new ConvexHexagons().find(7)); // 232
                  Console.WriteLine(new ConvexHexagons().find(104)); // 635471838
                  Console.WriteLine(new ConvexHexagons().find(15285)); // 339664453

                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
            }
      }
}
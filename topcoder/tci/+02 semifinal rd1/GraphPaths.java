import java.math.*;

      public class GraphPaths {
            public long howMany(String[] graph, int start, int destination, int length) {
                  BigInteger[][] g = new BigInteger[graph.length][graph.length];
                  for (int i = 0; i < graph.length; ++i) {
                        for (int j = 0; j < graph.length; ++j) {
                              g[i][j] = BigInteger.ZERO;
                        }
                        String[] adj = graph[i].split(" ");
                        for (int j = 0; j < adj.length; ++j) {
                              g[i][Integer.parseInt(adj[j])] = BigInteger.ONE;
                        }
                  }
                  g = pow(g, graph.length, length);
                  if (g[start][destination].compareTo(bound) > 0) {
                        return -1;
                  }
                  return g[start][destination].longValue();
            }

            private final BigInteger bound = BigInteger.valueOf(9223372036854775807L);
            
            private BigInteger[][] pow(BigInteger[][] a, int n, int k) {
                  if (k == 1) {
                        return a;
                  }
                  else if (k % 2 == 0) {
                        return pow(mul(a, a, n), n, k >> 1);
                  }
                  else {
                        return mul(a, pow(a, n, k - 1), n);
                  }
            }

            private BigInteger[][] mul(BigInteger[][] a, BigInteger[][] b, int n) {
                  BigInteger[][] c = new BigInteger[n][n];
                  for (int i = 0; i < n; ++i) {
                        for (int j = 0; j < n; ++j) {
                              c[i][j] = BigInteger.ZERO;
                              for (int k = 0; k < n; ++k) {
                                    c[i][j] = c[i][j].add(a[i][k].multiply(b[k][j]));
                              }
                              if (c[i][j].compareTo(bound) > 0) {
                                    c[i][j] = bound.add(bound);
                              }
                        }
                  }
                  return c;
            }

            public static void main(String args[]) {
                  System.out.println(new GraphPaths().howMany(new String[] {"1 2 3", "0 1 2", "0", "1 2"}, 0, 1, 2));
                  System.out.println(new GraphPaths().howMany(new String[] {"1", "2", "3", "4", "5", "0 3"}, 0, 0, 10000001));
                  System.out.println(new GraphPaths().howMany(new String[] {"1 2", "0", "0"}, 0, 0, 126));
                  System.out.println(new GraphPaths().howMany(new String[] {"0 1 2 3", "0 1 2 3", "0 1 2 3", "0 1 2 3"}, 2, 2, 63));
                  System.out.println(new GraphPaths().howMany(new String[] {"0"}, 0, 0, 1));
            }
      }
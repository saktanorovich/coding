using System;
using System.Collections.Generic;

public class ImpossibleGame {

      private static class CombinatoricUtils {
            private const int _count = 30;
            private static long[,] _binom = new long[_count + 1, _count + 1];
            private static bool _initialized = false;

            public static long combination(int set, int subset) {
                  if (!_initialized) {
                        _binom[0, 0] = 1;
                        for (int i = 1; i <= _count; ++i) {
                              _binom[i, 0] = 1;
                              for (int j = 1; j <= _count; ++j) {
                                    _binom[i, j] = _binom[i - 1, j] + _binom[i - 1, j - 1];
                              }
                        }
                        _initialized = true;
                  }
                  return _binom[set, subset];
            }
      }

      private class VertexRegister {
            private int[,,] _map;
            private int _id;

            public VertexRegister(int n) {
                  _map = new int[n + 1, n + 1, n + 1];
                  _id = 0;
            }

            public void register(int a, int b, int c) {
                  _map[a, b, c] = _id;
                  ++_id;
            }

            public int this[int a, int b, int c] {
                  get {
                        return _map[a, b, c];
                  }
            }

            public int verticesCount() {
                  return _id;
            }
      }

      private abstract class SCCAlgorithm {
            protected abstract List<List<int>> coreGetComponents(List<int>[] graph);
            
            public SCCAlgorithm() {
            }
            
            public List<List<int>> getComponents(List<int>[] graph) {
                  return coreGetComponents(graph);
            }
      };

      private class TarjanSCCAlgorithm : SCCAlgorithm {
            #region Fields

            private int _time;
            private int[] _time_in, _low;
            private bool[] _used, _in_stack;
            private Stack<int> _stack;

            #endregion

            #region Internal methods

            private void dfs(List<int>[]graph, int source, List<List<int>> components) {
                  _used[source] = true;
                  _time_in[source] = _low[source] = ++_time;
                  _stack.Push(source);
                  _in_stack[source] = true;
                  foreach (int destination in graph[source]) {
                        if (!_used[destination]) {
                              dfs(graph, destination, components);
                              _low[source] = Math.Min(_low[source], _low[destination]);
                        }
                        else if (_in_stack[destination]) {
                              _low[source] = Math.Min(_low[source], _time_in[destination]); 
                        }
                  }
                  if (_time_in[source] == _low[source]) {
                        List<int> component = new List<int>();
                        for (int u = -1; u != source; _stack.Pop(), _in_stack[u] = false) {
                              u = _stack.Peek();
                              component.Add(u);
                        }
                        components.Add(component);
                  }
            }

            protected override List<List<int>> coreGetComponents(List<int>[] graph) {
                  List<List<int>> result = new List<List<int>>();
                  _time_in = new int[graph.Length];
                  _low = new int[graph.Length];
                  _used = new bool[graph.Length];
                  _in_stack = new bool[graph.Length];
                  _time = 0;
                  for (int i = 0; i < graph.Length; ++i) {
                        if (!_used[i]) {
                              dfs(graph, i, result);
                        }
                  }
                  return result;
            }

            #endregion

            #region Public methods

            public TarjanSCCAlgorithm()
                  : base() {
                  _stack = new Stack<int>();
            }

            #endregion
      }

      private class CondensationBuilder {
            #region Fields

            private SCCAlgorithm _algorithm;
            private List<List<int>> _components;
            private List<int>[] _condensation;
            private int[] _componentById;

            #endregion

            #region Public methods

            public CondensationBuilder(SCCAlgorithm algorithm) {
                  _algorithm = algorithm;
            }

            public CondensationBuilder buildComponents(List<int>[] graph) {
                  _components = _algorithm.getComponents(graph);
                  return this;
            }

            public CondensationBuilder buildCandidates(List<int>[] graph) {
                  _componentById = new int[graph.Length];
                  for (int i = 0; i < _components.Count; ++i) {
                        for (int j = 0; j < _components[i].Count; ++j) {
                              _componentById[_components[i][j]] = i;
                        }
                  }
                  return this;
            }

            public CondensationBuilder buildCondensation(List<int>[] graph) {
                  _condensation = new List<int>[_components.Count];
                  for (int i = 0; i < _components.Count; ++i) {
                        _condensation[i] = new List<int>();
                  }
                  for (int source = 0; source < graph.Length; ++source) {
                        foreach (int destination in graph[source]) {
                              if (_componentById[source] != _componentById[destination]) {
                                    _condensation[_componentById[source]].Add(_componentById[destination]);
                              }
                        }
                  }
                  return this;
            }

            public List<List<int>> Components {
                  get {
                        return _components;
                  }
            }

            public List<int>[] Condensation {
                  get {
                        return _condensation;
                  }
            }

            public int getComponentBy(int id) {
                  return _componentById[id];
            }

            #endregion
      }

      private class TopologicalSorter {
            #region Fields

            private int[] _in;
            private Queue<int> _queue;

            #endregion

            #region Public methods

            public TopologicalSorter() {
            }

            public int[] Sort(List<int>[] graph) {
                  _in = new int[graph.Length];
                  for (int source = 0; source < graph.Length; ++source) {
                        foreach (int destination in graph[source]) {
                              ++_in[destination];
                        }
                  }
                  _queue = new Queue<int>();
                  for (int source = 0; source < graph.Length; ++source) {
                        if (_in[source] == 0) {
                              _queue.Enqueue(source);
                        }
                  }
                  List<int> result = new List<int>();
                  while (_queue.Count > 0) {
                        int source = _queue.Dequeue();
                        result.Add(source);
                        foreach (int destination in graph[source]) {
                              --_in[destination];
                              if (_in[destination] == 0) {
                                    _queue.Enqueue(destination);
                              }
                        }
                  }
                  return result.ToArray();
            }

            #endregion
      }

      public long getMinimum(int k, string[] before, string[] after_) {
            VertexRegister reg = new VertexRegister(k);
            for (int a = 0; a <= k; ++a) {
                  for (int b = 0; a + b <= k; ++b) {
                        for (int c = 0; a + b + c <= k; ++c) {
                              reg.register(a, b, c);
                        }
                  }
            }
            List<int>[] graph = new List<int>[reg.verticesCount()];
            for (int i = 0; i < reg.verticesCount(); ++i) {
                  graph[i] = new List<int>();
            }
            for (int a = 0; a <= k; ++a) {
                  for (int b = 0; a + b <= k; ++b) {
                        for (int c = 0; a + b + c <= k; ++c) {
                              int[] cnt = new int[4] { a, b, c, k - a - b - c };
                              for (int i = 0; i < before.Length; ++i) {
                                    int[] dec = new int[4];
                                    int[] inc = new int[4];
                                    for (int j = 0; j < before[i].Length; ++j) {
                                          ++dec[before[i][j] - 'A'];
                                          ++inc[after_[i][j] - 'A'];
                                    }
                                    bool ok = true;
                                    for (int j = 0; j < 4; ++j) {
                                          if (cnt[j] < dec[j]) {
                                                ok = false;
                                                break;
                                          }
                                    }
                                    if (ok) {
                                          for (int j = 0; j < 4; ++j) {
                                                inc[j] += cnt[j] - dec[j];
                                          }
                                          int p = reg[cnt[0], cnt[1], cnt[2]];
                                          int q = reg[inc[0], inc[1], inc[2]];
                                          if (p != q) {
                                                graph[p].Add(q);
                                          }
                                    }
                              }
                        }
                  }
            }
            CondensationBuilder builder = new CondensationBuilder(new TarjanSCCAlgorithm());
            builder.buildComponents(graph).
                        buildCandidates(graph).
                              buildCondensation(graph);
            long[] componentCost = new long[builder.Components.Count];
            for (int a = 0; a <= k; ++a) {
                  for (int b = 0; a + b <= k; ++b) {
                        for (int c = 0; a + b + c <= k; ++c) {
                              int id = reg[a, b, c];
                              componentCost[builder.getComponentBy(id)] += getPermutationsCount(k, a, b, c);
                        }
                  }
            }
            int[] order = new TopologicalSorter().Sort(builder.Condensation);
            bool[] used = new bool[builder.Components.Count];
            long[] dp = new long[builder.Components.Count];
            for (int i = 0; i < builder.Condensation.Length; ++i) {
                  int source = order[i];
                  if (!used[source]) {
                        dfs(builder.Condensation, source, used, componentCost, dp);
                  }
            }
            long result = 0;
            for (int i = 0; i < builder.Components.Count; ++i) {
                  result = Math.Max(result, dp[i]);
            }
            return result;
      }

      private void dfs(List<int>[] graph, int source, bool[] used, long[] componentCost, long[] dp) {
            used[source] = true;
            foreach (int destination in graph[source]) {
                  if (!used[destination]) {
                        dfs(graph, destination, used, componentCost, dp);
                  }
                  dp[source] = Math.Max(dp[source], dp[destination]);
            }
            dp[source] += componentCost[source];
      }

      private long getPermutationsCount(int k, int a, int b, int c) {
            return CombinatoricUtils.combination(k, a) * CombinatoricUtils.combination(k - a, b) * CombinatoricUtils.combination(k - a - b, c);
      }

      public static void Main(string[] args) {
            ImpossibleGame game = new ImpossibleGame();

            //Console.WriteLine(game.getMinimum(1, new string[] { "A" }, new string[] { "B" })); // 2
            //Console.WriteLine(game.getMinimum(2, new string[] { "A", "A", "D" }, new string[] { "B", "C", "D" })); // 5
            //Console.WriteLine(game.getMinimum(2, new string[] { "B", "C", "D" }, new string[] { "C", "D", "B" })); // 9
            //Console.WriteLine(game.getMinimum(6, new string[] { "AABBC", "AAAADA", "AAACA", "CABAA", "AAAAAA", "BAAAA" }, new string[] { "AACCB", "DAAABC", "AAAAD", "ABCBA", "AABAAA", "AACAA" })); // 499
            Console.WriteLine(game.getMinimum(5, new string[] {"A", "DB", "C", "D"}, new string[] {"B", "CD", "D", "D"})); // 421

            Console.ReadKey();
      }
}

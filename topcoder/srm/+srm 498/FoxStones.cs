using System;
using System.Collections.Generic;

public class FoxStones
{
      private const long modulo = 1000000009;

      public class State : IComparable
      {
            private int[] _state;

            public State(int[] state)
            {
                  _state = new int[state.Length];
                  for (int i = 0; i < state.Length; ++i)
                        _state[i] = state[i];
            }

            public int Length
            {
                  get { return _state.Length; }
            }

            public int this[int ix]
            {
                  get { return _state[ix]; }
            }

            public override bool Equals(object obj)
            {
                  if (obj is State)
                  {
                        State state = (State)obj;
                        if (this.Length == state.Length)
                        {
                              for (int i = 0; i < this.Length; ++i)
                                    if (_state[i] != state[i])
                                          return false;
                              return true;
                        }
                  }
                  return false;
            }

            public override int GetHashCode()
            {
                  return this.ToString().GetHashCode();
            }

            public override string ToString()
            {
                  string result = string.Empty;
                  for (int i = 0; i < _state.Length - 1; ++i)
                        result += _state[i] + ",";
                  result += _state[_state.Length - 1];
                  return result;
            }

            #region IComparable Members

            public int CompareTo(object obj)
            {
                  if (obj is State)
                  {
                        State state = (State)obj;
                        for (int i = 0; i < _state.Length; ++i)
                              if (_state[i] != state[i])
                                    return (_state[i] - state[i]);
                        return 0;
                  }
                  return -1;
            }

            #endregion
      }

      public int getCount(int n, int m, int[] sx, int[] sy)
      {
            SortedDictionary<State, int> cache = new SortedDictionary<State, int>();
            for (int i = 1; i <= n; ++i)
                  for (int j = 1; j <= m; ++j)
                        if (!isMarked(i, j, sx, sy))
                        {
                              int[] _state = new int[sx.Length];
                              for (int k = 0; k < sx.Length; ++k)
                                    _state[k] = distance(i, j, sx[k], sy[k]);
                              State state = new State(_state);
                              if (!cache.ContainsKey(state))
                                    cache.Add(state, 0);
                              ++cache[state];
                        }
            long result = 1;
            foreach (KeyValuePair<State, int> pair in cache)
                  result = (result * fact(pair.Value, modulo)) % modulo;
            return (int)result;
      }

      private long fact(int k, long modulo)
      {
            long result = 1;
            for (int i = 2; i <= k; ++i)
                  result = (result * i) % modulo;
            return result;
      }

      private int distance(int x1, int y1, int x2, int y2)
      {
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
      }

      private bool isMarked(int x, int y, int[] sx, int[] sy)
      {
            for (int i = 0; i < sx.Length; ++i)
                  if (sx[i] == x && sy[i] == y)
                        return true;
            return false;
      }

      static void Main(string[] args)
      {
            Console.WriteLine(new FoxStones().getCount(6, 1, new int[] { 3 }, new int[] { 1 }));
            Console.WriteLine(new FoxStones().getCount(2, 2, new int[] { 2 }, new int[] { 1 }));
            Console.WriteLine(new FoxStones().getCount(3, 3, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }));
            Console.WriteLine(new FoxStones().getCount(12, 34, new int[] { 5, 6, 7, 8, 9, 10 }, new int[] { 11, 12, 13, 14, 15, 16 }));
            Console.WriteLine(new FoxStones().getCount(200, 200, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 }));

            Console.ReadLine();
      }
}

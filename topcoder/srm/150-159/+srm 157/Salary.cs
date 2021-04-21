using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Salary {
            public int howMuch(string[] arrival, string[] departure, int wage) {
                  long[] cost = new long[24 * 60 * 60];
                  for (int sec = getSecond("00:00:00"); sec < getSecond("06:00:00"); ++sec) cost[sec] = 3 * wage;
                  for (int sec = getSecond("18:00:00"); sec < getSecond("23:59:59"); ++sec) cost[sec] = 3 * wage;
                  for (int sec = getSecond("06:00:00"); sec < getSecond("18:00:00"); ++sec) cost[sec] = 2 * wage;
                  long result = 0;
                  for (int i = 0; i < arrival.Length; ++i) {
                        for (int sec = getSecond(arrival[i]); sec < getSecond(departure[i]); ++sec) {
                              result += cost[sec];
                        }
                  }
                  return (int)(result / 7200);
            }

            private int getSecond(string timeStamp) {
                  return (int)TimeSpan.Parse(timeStamp).TotalSeconds;
            }
      }
}
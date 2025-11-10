using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class Iditarod {
            public int avgMinutes(string[] times) {
                  double result = 0;
                  foreach (string timeEntry in times) {
                        string[] items = timeEntry.Split(
                              new string[] { " ", ":", ",", "DAY" },
                                    StringSplitOptions.RemoveEmptyEntries);
                        int hh = int.Parse(items[0]);
                        int mm = int.Parse(items[1]);
                        int dd = int.Parse(items[3]);
                        if (items[2] == "AM") {
                              if (hh == 12) {
                                    hh = 0;
                              }
                        }
                        if (items[2] == "PM") {
                              if (hh != 12) {
                                    hh += 12;
                              }
                        }
                        result += (hh + (dd - 1) * 24) * 60 + mm - 480;
                  }
                  return (int)(result / times.Length + 0.5);
            }
      }
}
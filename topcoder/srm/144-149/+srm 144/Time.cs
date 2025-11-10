using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Time {
            public string whatTime(int seconds) {
                  TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
                  return string.Format("{0}:{1}:{2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            }
      }
}
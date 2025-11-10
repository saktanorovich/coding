using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Traffic {
            public int time(int[] lights, int speed) {
                  double time = length / speed;
                  for (int i = 0; i < lights.Length; ++i) {
                        time = getTime(lights[i], time) + length / speed;
                  }
                  return (int)Math.Floor(time);
            }

            private double getTime(double light, double time) {
                  bool green = true;
                  for (double t = 0; true; t += light) {
                        if (t <= time && time < t + light) {
                              return green ? time : t + light;
                        }
                        green = !green;
                  }
            }

            private double length = 150.0;
      }
}
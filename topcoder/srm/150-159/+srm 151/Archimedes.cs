using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Archimedes {
            public double approximatePi(int numSides) {
                  return numSides * Math.Sin(Math.PI / numSides);
            }
      }
}
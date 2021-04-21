using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class PeopleCircle {
            public string order(int numMales, int numFemales, int k) {
                  char[] circle = new char[numMales + numFemales];
                  List<int> positions = new List<int>();
                  for (int index = 0; index < numMales + numFemales; ++index) {
                        circle[index] = 'M';
                        positions.Add(index);
                  }
                  for (int index = 0, round = 1; round <= numFemales; ++round) {
                        index = (index + k - 1) % positions.Count;
                        circle[positions[index]] = 'F';
                        positions.RemoveAt(index);
                  }
                  return new string(circle);
            }
      }
}
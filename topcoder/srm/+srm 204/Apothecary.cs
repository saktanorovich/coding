using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCoder.Algorithm {
    public class Apothecary {
        public int[] balance(int W) {
            var res = new List<int>();
            for (var grain = 1; W > 0; grain *= 3) {
                switch (W % 3) {
                    case 1:
                        res.Add(+grain);
                        W = W - 1;
                        break;
                    case 2:
                        res.Add(-grain);
                        W = W + 1;
                        break;
                }
                W /= 3;
            }
            return res.OrderBy(x => x).ToArray();
        }
    }
}

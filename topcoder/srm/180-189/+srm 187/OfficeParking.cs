using System;
using System.Collections.Generic;
using System.Linq;

namespace TopCoder.Algorithm {
    public class OfficeParking {
        public int spacesUsed(string[] events) {
            return spacesUsed(Array.ConvertAll(Array.ConvertAll(events, e => e.Split(' ')),
                data => {
                    if (data[1] == "arrives") {
                        return +1;
                    }
                    return -1;
            }));
        }

        private int spacesUsed(int[] events) {
            int result = 0, attime = 0;
            for (var i = 0; i < events.Length; ++i) {
                attime += events[i];
                result = Math.Max(result, attime);
            }
            return result;
        }
    }
}
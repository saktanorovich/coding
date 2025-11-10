using System;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
    public class SearchDisks {
        public int numberToTakeOut(string diskNames, string searchingDisk) {
            return numberToTakeOut(diskNames.Split(' '), searchingDisk);
        }

        private static int numberToTakeOut(string[] diskNames, string searchingDisk) {
            for (var i = diskNames.Length - 1; i >= 0; --i) {
                if (diskNames[i] == searchingDisk) {
                    return diskNames.Length - 1 - i;
                }
            }
            return int.MaxValue;
        }
    }
}
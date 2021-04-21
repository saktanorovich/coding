using System;

namespace TopCoder.Algorithm {
    public class BoxLoader {
        public int mostItems(int boxX, int boxY, int boxZ, int itemX, int itemY, int itemZ) {
            var result = 0;
            result = Math.Max(result, count(boxX, boxY, boxZ, itemX, itemY, itemZ));
            result = Math.Max(result, count(boxX, boxY, boxZ, itemX, itemZ, itemY));
            result = Math.Max(result, count(boxX, boxY, boxZ, itemY, itemX, itemZ));
            result = Math.Max(result, count(boxX, boxY, boxZ, itemY, itemZ, itemX));
            result = Math.Max(result, count(boxX, boxY, boxZ, itemZ, itemX, itemY));
            result = Math.Max(result, count(boxX, boxY, boxZ, itemZ, itemY, itemX));
            return result;
        }

        private int count(int boxX, int boxY, int boxZ, int itemX, int itemY, int itemZ) {
            return (boxX / itemX) *
                   (boxY / itemY) *
                   (boxZ / itemZ);
        }
    }
}
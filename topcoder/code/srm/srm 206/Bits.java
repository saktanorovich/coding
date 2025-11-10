import java.util.*;

    public class Bits {
        public int minBits(int n) {
            if (n > 0) {
                return 1 + minBits(n >> 1);
            }
            return 0;
        }
    }

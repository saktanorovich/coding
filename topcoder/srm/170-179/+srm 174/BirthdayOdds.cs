using System;

namespace TopCoder.Algorithm {
    public class BirthdayOdds {
        public int minPeople(int minOdds, int daysInYear) {
            for (var people = 1; ; ++people) {
                var different = 1.0;
                for (var count = 0; count < people; ++count) {
                    different *= 1.0 * (daysInYear - count) / daysInYear;
                }
                if (1 - different >= minOdds / 100.0) {
                    return people;
                }
            }
        }
    }
}
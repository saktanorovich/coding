using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class CrazyCompetition {
        public double differenceTemperature(int daysSummer, int daysWinter, string[] temperature) {
            return differenceTemperature(daysSummer, daysWinter,
                string.Join("", temperature).ToCharArray().Select(day => {
                    if (day == '0') {
                        return 0;
                    }
                    if (Positive.IndexOf(day) >= 0) {
                        return +Positive.IndexOf(day);
                    }
                    return -Negative.IndexOf(day);
                }).ToArray());
        }

        private double differenceTemperature(int daysSummer, int daysWinter, int[] temperature) {
            cumul[0] = temperature[0];
            for (var day = 1; day < temperature.Length; ++day) {
                cumul[day] = cumul[day - 1] + temperature[day];
            }
            var result = -1e12;
            for (var summer = 0; summer + daysSummer <= temperature.Length; ++summer) {
                for (var winter = summer + daysSummer; winter + daysWinter <= temperature.Length; ++winter) {
                    result = Math.Max(result, avg(summer, summer + daysSummer - 1) - avg(winter, winter + daysWinter - 1));
                }
            }
            for (var winter = 0; winter + daysWinter <= temperature.Length; ++winter) {
                for (var summer = winter + daysWinter; summer + daysSummer <= temperature.Length; ++summer) {
                    result = Math.Max(result, avg(summer, summer + daysSummer - 1) - avg(winter, winter + daysWinter - 1));
                }
            }
            return result;
        }

        private double avg(int first, int last) {
            double result = cumul[last];
            if (first > 0) {
                result -= cumul[first - 1];
            }
            return result / (last - first + 1);
        }

        private readonly int[] cumul = new int[2500];

        private static readonly string Positive = " abcdefghijklmnopqrstuvwxyz";
        private static readonly string Negative = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}
using System;
using System.Globalization;

namespace TopCoder.Algorithm {
    public class CalendarISO {
        public int weekNumber(int year, int month, int day) {
            return new GenevaCalendar().GetWeekOfYear(new DateTime(year, month, day));
        }

        private sealed class GenevaCalendar : GregorianCalendar {

            public int GetWeekOfYear(DateTime date) {
                if (date == DateTime.MaxValue.Date) {
                    return 1;
                }
                /* first of all find the nearest Thursday because Thursday determines the first week of the year.. */
                while (Gt(GetDayOfWeek(date))) date = date.AddDays(-1);
                while (Le(GetDayOfWeek(date))) date = date.AddDays(+1);
                /* it may happen that original date year and Thurday year are different so we need to find
                 * the first Thurday in the year related to the Thurday nearest to the date.. */
                DateTime firstThurday = new DateTime(date.Year, 1, 1);
                while (GetDayOfWeek(firstThurday) != DayOfWeek.Thursday) {
                    firstThurday = firstThurday.AddDays(1);
                }
                int daysBetweenThursdays = (date - firstThurday).Days;
                if (daysBetweenThursdays < 0) {
                    daysBetweenThursdays = -daysBetweenThursdays;
                }
                return (daysBetweenThursdays / 7) + 1;
            }

            public override DayOfWeek GetDayOfWeek(DateTime date) {
                var result = (int)base.GetDayOfWeek(date);
                result = (result + 3) % 7;
                return (DayOfWeek)result;
            }

            private static bool Le(DayOfWeek dayOfWeek) {
                switch (dayOfWeek) {
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                        return true;
                }
                return false;
            }

            private static bool Gt(DayOfWeek dayOfWeek) {
                switch (dayOfWeek) {
                    case DayOfWeek.Friday:
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        return true;
                }
                return false;
            }
        }
    }
}
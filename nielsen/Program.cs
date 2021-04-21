using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace nielsen.coding {
    public class Program {
        public static void Main(string[] args) {
            var hdrf = new HumanReadableDurationFormat();
            Console.WriteLine(hdrf.Format(0));
            Console.WriteLine(hdrf.Format(62));
            Console.WriteLine(hdrf.Format(365));
            Console.WriteLine(hdrf.Format(365 * 24 * 60 * 60));
            Console.WriteLine(hdrf.Format(24 * 60 * 60));
            Console.WriteLine(hdrf.Format(60 * 60));
            Console.WriteLine(hdrf.Format(60));
            Console.WriteLine(hdrf.Format(1));
            Console.WriteLine(hdrf.Format(100));
            Console.WriteLine(hdrf.Format(1000000));
            Console.WriteLine(hdrf.Format(int.MaxValue));
            Console.WriteLine(hdrf.Format(15731080));
        }
    }

    public class HumanReadableDurationFormat {
        public string Format(int duration) {
            if (duration > 0) {
                return format(duration);
            } else {
                return "now";
            }
        }

        private static string format(int duration) {
            var year = duration / YEAR; duration -= year * YEAR;
            var days = duration / DAYS; duration -= days * DAYS;
            var hour = duration / HOUR; duration -= hour * HOUR;
            var mins = duration / MINS; duration -= mins * MINS;
            var secs = duration;
            return format(year, days, hour, mins, secs);
        }

        private static string format(int year, int days, int hour, int mins, int secs) {
            var output = new List<string>();
            if (year > 0) output.Add(format("year"   , year));
            if (days > 0) output.Add(format("day"    , days));
            if (hour > 0) output.Add(format("hour"   , hour));
            if (mins > 0) output.Add(format("minute" , mins));
            if (secs > 0) output.Add(format("second" , secs));
            var result = string.Join(", ", output);
            result = result.Replace($", {output.Last()}", $" and {output.Last()}");
            return result;
        }

        private static string format(string text, int value) {
            return value > 1 ? $"{value} {text}s" : $"{value} {text}";
        }

        private const int YEAR = 60 * 60 * 24 * 365;
        private const int DAYS = 60 * 60 * 24;
        private const int HOUR = 60 * 60;
        private const int MINS = 60;
    }
}
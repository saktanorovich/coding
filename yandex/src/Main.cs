using System;
using System.Diagnostics;
using System.IO;
using yandex.blitz2017.finals;
using yandex.blitz2017.qual;

namespace yandex {
    public class Program {
        public static void Main(string[] args) {
            var reader = GetReader("input.txt");
            var writer = GetWriter("output.txt");
            //var reader = GetReader(null);
            //var writer = GetWriter(null);

            if (args.Length > 0 && args[0] == "-g") {
            }
            else {
                Console.Error.WriteLine("Test Case: Elapsed time");
                var contd = true;
                for (var test = 1; !reader.EndOfStream && contd; ++test) {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    contd = new ProblemF().process(test, reader, writer);
                    writer.Flush();
                    stopwatch.Stop();
                    Console.Error.WriteLine("Test #{0,-3}: {1,9:###,##0} ms", test, stopwatch.ElapsedMilliseconds);
                }
            }
            reader.Dispose();
            writer.Dispose();
        }

        private static StreamReader GetReader(string source) {
            if (String.IsNullOrWhiteSpace(source)) {
                return new StreamReader(Console.OpenStandardInput());
            }
            return new StreamReader(new FileStream(source, FileMode.OpenOrCreate, FileAccess.Read));
        }

        private static StreamWriter GetWriter(string source) {
            if (String.IsNullOrWhiteSpace(source)) {
                return new StreamWriter(Console.OpenStandardOutput());
            }
            return new StreamWriter(new FileStream(source, FileMode.Create, FileAccess.Write));
        }
    }
}

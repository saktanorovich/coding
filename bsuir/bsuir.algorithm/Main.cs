using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace bsuir.algorithm {
    public class Program {
        public static void Main(string[] args) {
            var reader = InputReader.FromSource(@"..\..\input.txt");
            var writer = OutputWriter.FromSource(@"..\..\output.txt");
            //var reader = InputReader.FromSource(null);
            //var writer = OutputWriter.FromSource(null);

            if (args.Length > 0 && args[0] == "-g") {
                var rand = new Random(50847534);
                for (var test = 0; test < 10; ++test) {
                }
            }
            else {
                Console.Error.WriteLine("Test Case: Elapsed time");
                var contd = true;
                for (var test = 1; reader.HasNext() && contd; ++test) {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    contd = new Problem_03().process(test, reader, writer);
                    writer.Flush();
                    stopwatch.Stop();
                    Console.Error.WriteLine("Test #{0,-3}: {1,9:###,##0} ms", test, stopwatch.ElapsedMilliseconds);
                }
            }
            reader.Dispose();
            writer.Dispose();
        }
    }

    public class InputReader : IDisposable {
        private readonly StreamReader reader;
        private string token;

        public InputReader(StreamReader reader) {
            this.reader = reader;
        }

        public bool HasNext() {
            while (token == null) {
                var builder = new StringBuilder();
                while (reader.EndOfStream == false) {
                    var c = (char)reader.Peek();
                    if (char.IsWhiteSpace(c)) {
                        reader.Read();
                    } else {
                        break;
                    }
                }
                while (reader.EndOfStream == false) {
                    var c = (char)reader.Peek();
                    if (char.IsWhiteSpace(c) == false) {
                        builder.Append((char)reader.Read());
                    } else {
                        break;
                    }
                }
                token = builder.ToString();
            }
            return !string.IsNullOrEmpty(token);
        }

        public string Next() {
            if (HasNext()) {
                var res = token;
                token = null;
                return res;
            }
            return null;
        }

        public int NextInt() {
            return int.Parse(Next());
        }

        public long NextLong() {
            return long.Parse(Next());
        }

        public void Dispose() {
            reader?.Dispose();
        }

        public static InputReader FromSource(string source) {
            if (String.IsNullOrWhiteSpace(source)) {
                return new InputReader(new StreamReader(Console.OpenStandardInput()));
            }
            return new InputReader(new StreamReader(new FileStream(source, FileMode.OpenOrCreate, FileAccess.Read)));
        }
    }

    public class OutputWriter : IDisposable {
        private readonly StreamWriter writer;

        public OutputWriter(StreamWriter writer) {
            this.writer = writer;
        }

        public void Write(char c) {
            writer.Write(c);
        }

        public void Write(int x) {
            writer.Write(x);
        }

        public void Write(string format, params object[] args) {
            writer.Write(format, args);
        }

        public void WriteLine() {
            writer.WriteLine();
        }

        public void WriteLine(int x) {
            writer.WriteLine(x);
        }

        public void WriteLine(long x) {
            writer.WriteLine(x);
        }

        public void WriteLine(string format, params object[] args) {
            writer.WriteLine(format, args);
        }

        public void Flush() {
            writer.Flush();
        }

        public void Dispose() {
            writer?.Dispose();
        }

        public static OutputWriter FromSource(string source) {
            if (String.IsNullOrWhiteSpace(source)) {
                return new OutputWriter(new StreamWriter(Console.OpenStandardOutput()));
            }
            return new OutputWriter(new StreamWriter(new FileStream(source, FileMode.Create, FileAccess.Write)));
        }
    }
}

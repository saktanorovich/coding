using System;

    class TwoRotationCypher
    {
        private const string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public string encrypt(int firstSize, int firstRotate, int secondRotate, string message)
        {
            int secondSize = alphabet.Length - firstSize;
            string first = string.Empty;
            string second = string.Empty;
            for (int i = 0; i < firstSize; ++i)
            {
                first += alphabet[i].ToString();
            }
            for (int i = firstSize; i < alphabet.Length; ++i)
            {
                second += alphabet[i].ToString();
            }
            string result = string.Empty;
            for (int i = 0; i < message.Length; ++i)
            {
                if (message[i].Equals(' '))
                {
                    result += message[i].ToString();
                }
                else
                {
                    if (first.IndexOf(message[i]) != -1)
                    {
                        result += first[(first.IndexOf(message[i]) + firstRotate) % firstSize].ToString();
                    }
                    else
                    {
                        result += second[(second.IndexOf(message[i]) + secondRotate) % secondSize].ToString();
                    }
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
            TwoRotationCypher trc = new TwoRotationCypher();
            Console.WriteLine(trc.encrypt(13, 0, 0, "this string will not change at all"));
            Console.WriteLine(trc.encrypt(13, 7, 0, "only the letters a to m in this string change"));
            Console.WriteLine(trc.encrypt(3, 1, 2, "  watch   out for strange  spacing "));

            Console.ReadLine();
        }
    }

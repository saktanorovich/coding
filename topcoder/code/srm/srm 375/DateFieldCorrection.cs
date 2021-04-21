using System;
using System.Collections.Generic;

    class DateFieldCorrection
    {
        private string[] months = new string[] { "January", "February", "March", "April", "May", 
            "June", "July", "August", "September", "October", "November", "December" };
        private int[] lengths = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private string alphabet = "1234567890qwertyuiopasdfghjklzxcvbnm ";
        private int[,] g;
        private void generate()
        {
            g = new int[alphabet.Length, alphabet.Length];
            for (int i = 0; i < alphabet.Length; ++i)
            {
                for (int j = 0; j < alphabet.Length; ++j)
                {
                    g[i, j] = int.MaxValue / 2;
                }
                g[i, i] = 0;
            }
            List<int[]> what = new List<int[]>();
            what.Add(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            what.Add(new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 });
            what.Add(new int[] { 20, 21, 22, 23, 24, 25, 26, 27, 28 });
            what.Add(new int[] { 29, 30, 31, 32, 33, 34, 35 });
            what.Add(new int[] { 36 });
            /* level 0 */
            for (int i = 0; i < what[0].Length - 1; ++i)
            {
                g[what[0][i], what[0][i + 1]] = 1;
                g[what[0][i + 1], what[0][i]] = 1;
            }
            for (int i = 0; i < what[0].Length; ++i)
            {
                g[what[0][i], what[1][i]] = 1;
                g[what[1][i], what[0][i]] = 1;
                if (i > 0)
                {
                    g[what[0][i], what[1][i - 1]] = 1;
                    g[what[1][i - 1], what[0][i]] = 1;
                }
            }
            /* level 1 */
            for (int i = 0; i < what[1].Length - 1; ++i)
            {
                g[what[1][i], what[1][i + 1]] = 1;
                g[what[1][i + 1], what[1][i]] = 1;
            }
            for (int i = 0; i < what[1].Length; ++i)
            {
                if (i != what[1].Length - 1)
                {
                    g[what[1][i], what[2][i]] = 1;
                    g[what[2][i], what[1][i]] = 1;
                }
                if (i != 0)
                {
                    g[what[1][i], what[2][i - 1]] = 1;
                    g[what[2][i - 1], what[1][i]] = 1;
                }
            }
            /* level 2 */
            for (int i = 0; i < what[2].Length - 1; ++i)
            {
                g[what[2][i], what[2][i + 1]] = 1;
                g[what[2][i + 1], what[2][i]] = 1;
            }
            for (int i = 0; i < what[2].Length - 1; ++i)
            {
                if (i < what[2].Length - 2)
                {
                    g[what[2][i], what[3][i]] = 1;
                    g[what[3][i], what[2][i]] = 1;
                }
                if (i != 0)
                {
                    g[what[2][i], what[3][i - 1]] = 1;
                    g[what[3][i - 1], what[2][i]] = 1;
                }
            }
            /* level 3 */
            for (int i = 0; i < what[3].Length - 1; ++i)
            {
                g[what[3][i], what[3][i + 1]] = 1;
                g[what[3][i + 1], what[3][i]] = 1;
            }
            for (int i = 1; i < what[3].Length; ++i)
            {
                g[what[3][i], what[4][0]] = 3;
                g[what[4][0], what[3][i]] = 3;
            }
            /* Floyd */
            for (int k = 0; k < alphabet.Length; ++k)
                for (int i = 0; i < alphabet.Length; ++i)
                    for (int j = 0; j < alphabet.Length; ++j)
                    {
                        g[i, j] = Math.Min(g[i, j], g[i, k] + g[k, j]);
                    }
        }
        private int cost(string s1, string s2)
        {
            s1 = s1.ToLower();
            s2 = s2.ToLower();
            int result = 0;
            for (int i = 0; i < s1.Length; ++i)
            {
                int v1 = alphabet.IndexOf(s1[i]);
                int v2 = alphabet.IndexOf(s2[i]);
                result += g[v1, v2];
            }
            return result;
        }
        public string correctDate(string input)
        {
            generate();
            int minCost = int.MaxValue / 2;
            string result = string.Empty;
            for (int i = 0; i < months.Length; ++i)
                for (int d = 1; d <= lengths[i]; ++d)
                {
                    string date = months[i] + " " + d.ToString();
                    if (input.Length == date.Length)
                    {
                        if (cost(input, date) < minCost)
                        {
                            minCost = cost(input, date);
                            result = date;
                        }
                    }
                }
            return result;
        }
        static void Main(string[] args)
        {
            DateFieldCorrection dfc = new DateFieldCorrection();
            Console.WriteLine(dfc.correctDate("Novebmer 10"));
            Console.WriteLine(dfc.correctDate("September 15"));
            Console.WriteLine(dfc.correctDate("Juny 4"));
            Console.WriteLine(dfc.correctDate("Juny 31"));
            Console.WriteLine(dfc.correctDate("TopCoder"));

            Console.ReadLine();
        }
    }

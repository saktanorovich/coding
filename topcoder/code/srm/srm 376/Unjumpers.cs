using System;
using System.Collections.Generic;

    class Unjumpers
    {
        private int[] ddz = new int[] { -1, -1, +1, -1, +1, -1 };
        private int[] ddo = new int[] { +1, -1, -1, -1, -1, +1 };
        private int[] ddt = new int[] { -1, +1, -1, +1, -1, -1 };
        private bool[, ,] states;
        private void run(int z, int o, int t)
        {
            if (z < 0 || o < 0 || t < 0 || z > 35 || o > 35 || t > 35)
            {
                return;
            }
            if (!states[z, o, t])
            {
                states[z, o, t] = true;
                for (int i = 0; i < 6; ++i)
                {
                    run(z + ddz[i], o + ddo[i], t + ddt[i]);
                }
                int lastz = (z > 0 ? (z - 1) * 3 : -1);
                int lasto = (o > 0 ? 1 + (o - 1) * 3 : -1);
                int lastt = (t > 0 ? 2 + (t - 1) * 3 : -1);
                int last = Math.Max(lastz, Math.Max(lasto, lastt));
                if (last == lastz)
                {
                    run(z - 1, o + 1, t + 1);
                }
                else if (last == lasto)
                {
                    run(z + 1, o - 1, t + 1);
                }
                else
                {
                    run(z + 1, o + 1, t - 1);
                }
            }
        }
        private void code(string s)
        {
            states = new bool[40, 40, 40];
            int[] count = new int[3];
            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i].Equals('*'))
                {
                    count[i % 3]++;
                }
            }
            run(count[0], count[1], count[2]);
        }
        public int reachableTargets(string start, string[] targets)
        {
            int result = 0;
            code(start);
            bool[, ,] codeStates = new bool[40, 40, 40];
            for (int z = 0; z < 40; ++z)
                for (int o = 0; o < 40; ++o)
                    for (int t = 0; t < 40; ++t)
                    {
                        codeStates[z, o, t] = states[z, o, t];
                    }
            foreach (string s in targets)
            {
                code(s);
                for (int z = 0; z < 40; ++z)
                    for (int o = 0; o < 40; ++o)
                        for (int t = 0; t < 40; ++t)
                        {
                            if (codeStates[z, o, t] && states[z, o, t])
                            {
                                ++result;
                                goto next;
                            }
                        }
                next: ;
            }
            return result;
        }
        static void Main(string[] args)
        {
            Unjumpers u = new Unjumpers();
            Console.WriteLine(u.reachableTargets("**.", new string[]{"..*", "*.**", ".*.*"}));
            Console.WriteLine(u.reachableTargets("..***", new string[]{"..****..*", "..***....", "..****"}));
            Console.WriteLine(u.reachableTargets("*..*", new string[]{"*..*......", "*.....*...", "...*.....*", 
                "...*..*...", "*........*", "*...***..*"}));
            Console.WriteLine(u.reachableTargets("...***", new string[]{"***...", "..****", "**....**", ".*.*.*"}));

            Console.ReadLine();
        }
    }

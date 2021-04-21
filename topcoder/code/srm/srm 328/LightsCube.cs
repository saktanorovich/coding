using System;

    class LightsCube
    {
        public int[] count(int n, string[] lights)
        {
            int[][] c = new int[lights.Length][];
            for (int i = 0; i < lights.Length; ++i)
            {
                c[i] = Array.ConvertAll(lights[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries),
                        (Converter<string, int>)delegate(string item)
                        {
                            return int.Parse(item);
                        });
            }
            int[] result = new int[lights.Length];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    for (int k = 0; k < n; ++k)
                    {
                        int min = int.MaxValue, color = int.MaxValue;
                        for (int r = 0; r < lights.Length; ++r)
                        {
                            int d = Math.Abs(i - c[r][0]) + Math.Abs(j - c[r][1]) + Math.Abs(k - c[r][2]);
                            if (d < min)
                            {
                                min = d;
                                color = r;
                            }
                        }
                        result[color]++;
                    }
                }
            }
            return result;
        }
        static void Main()
        {
            var result = new LightsCube().count(2, new string[] { "0 0 0", "0 0 1", "0 1 0", "0 1 1", "1 0 0", "1 0 1", "1 1 0", "1 1 1" });

            Console.ReadLine();
        }
    }

using System;

    class MovingAvg
    {
        public double difference(int k, double[] data)
        {
            double min = 1000000000000, max = 0;
            for (int i = 0; i <= data.Length - k; ++i)
            {
                double avg = 0;
                for (int j = i; j < i + k; ++j)
                {
                    avg += data[j];
                }
                avg /= k;
                min = Math.Min(avg, min);
                max = Math.Max(avg, max);
            }
            return max - min;
        }
        static void Main(string[] args)
        {
            MovingAvg ma = new MovingAvg();
            Console.WriteLine(ma.difference(2, new double[] { 3, 8, 9, 15 }));
            Console.WriteLine(ma.difference(3, new double[] { 17, 6.2, 19, 3.4 }));
            Console.WriteLine(ma.difference(3, new double[] { 6, 2.5, 3.5 }));

            Console.ReadLine();
        }
    }

using System;

    class MonstersAndBunnies
    {
        public double survivalProbability(int monsters, int bunnies)
        {
            if (monsters % 2 == 1)
            {
                return 0.0;
            }
            double result = 1.0;
            while (monsters > 0)
            {
                result *= (double)(monsters - 1) / (monsters + 1);
                monsters -= 2;
            }
            return result;
        }
        static void Main(string[] args)
        {
            MonstersAndBunnies mab = new MonstersAndBunnies();
            Console.WriteLine(mab.survivalProbability(2, 0));

            Console.ReadLine();
        }
    }

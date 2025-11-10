using System;
using System.Collections.Generic;

class NewAlbum
{
    public int leastAmountOfCDs(int nSongs, int length, int cdCapacity)
    {
        if (length > cdCapacity)
        {
            return 0;
        }
        int songsPerCd = cdCapacity / (length + 1);
        if (cdCapacity - songsPerCd * (length + 1) >= length)
        {
            songsPerCd++;
        }
        int[] dp = new int[nSongs + 1];
        for (int i = 1; i <= nSongs; ++i)
        {
            dp[i] = int.MaxValue / 2;
            for (int j = 1; j <= Math.Min(i, songsPerCd); ++j)
            {
                if (j % 13 != 0)
                {
                    dp[i] = Math.Min(dp[i], dp[i - j] + 1);
                }
            }
        }
        return dp[nSongs];
    }
    static void Main(string[] args)
    {
        NewAlbum na = new NewAlbum();
        Console.WriteLine(na.leastAmountOfCDs(7, 2, 6)); //  4  Passed  
        Console.WriteLine(na.leastAmountOfCDs(20, 1, 100)); //  1  Passed  
        Console.WriteLine(na.leastAmountOfCDs(26, 1, 100)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(26, 3, 51)); //  3  Passed  
        Console.WriteLine(na.leastAmountOfCDs(67, 271, 1000)); //  23  Passed  
        Console.WriteLine(na.leastAmountOfCDs(27, 1, 27)); //  3  Passed  
        Console.WriteLine(na.leastAmountOfCDs(63, 1, 99)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(38, 27, 705)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(61, 12, 319)); //  3  Passed  
        Console.WriteLine(na.leastAmountOfCDs(31, 25, 483)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(75, 5, 373)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(85, 19, 923)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(90, 19, 642)); //  3  Passed  
        Console.WriteLine(na.leastAmountOfCDs(67, 37, 712)); //  4  Passed  
        Console.WriteLine(na.leastAmountOfCDs(63, 30, 821)); //  3  Passed  
        Console.WriteLine(na.leastAmountOfCDs(58, 4, 163)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(48, 20, 740)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(69, 6, 304)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(74, 9, 616)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(58, 11, 390)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(97, 20, 602)); //  4  Passed  
        Console.WriteLine(na.leastAmountOfCDs(61, 42, 701)); //  4  Passed  
        Console.WriteLine(na.leastAmountOfCDs(54, 13, 586)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(54, 9, 279)); //  2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(77, 50, 838)); // 5  Passed  
        Console.WriteLine(na.leastAmountOfCDs(47, 5, 102)); //  3  Passed  
        Console.WriteLine(na.leastAmountOfCDs(28, 59, 919)); // 2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(79, 7, 175)); //  4  Passed  
        Console.WriteLine(na.leastAmountOfCDs(29, 50, 858)); // 2  Passed  
        Console.WriteLine(na.leastAmountOfCDs(100, 10000, 10000)); // 100  Passed  
        Console.WriteLine(na.leastAmountOfCDs(100, 1, 10000)); // 1  Passed  
        Console.WriteLine(na.leastAmountOfCDs(79, 89, 264)); // 40  Passed  
        Console.WriteLine(na.leastAmountOfCDs(100, 89, 582)); // 17  Passed  
        Console.WriteLine(na.leastAmountOfCDs(100, 657, 10000)); // 7  Passed  
        Console.WriteLine(na.leastAmountOfCDs(100, 687, 9572)); // 9  Passed  
        Console.WriteLine(na.leastAmountOfCDs(100, 10, 150)); // 9  Passed  
        Console.WriteLine(na.leastAmountOfCDs(1, 1, 1)); // 1  Passed  
        Console.WriteLine(na.leastAmountOfCDs(1, 1, 10000)); // 1  Passed  
        Console.WriteLine(na.leastAmountOfCDs(1, 10000, 10000)); // 1  Passed  
        Console.WriteLine(na.leastAmountOfCDs(1, 13, 13)); // 1  Passed  
        Console.WriteLine(na.leastAmountOfCDs(1, 12, 13)); // 1  

        Console.ReadLine();
    }
}

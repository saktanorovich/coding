using System;
using System.Collections.Generic;
 
public class WordTrain
{
    public class Car : IComparable<Car>
    {
        public string Content;
        public char Front;
        public char End;

        public Car(string content, char front, char end)
        {
            Content = content;
            Front = front;
            End = end;
        }

        public int CompareTo(Car other)
        {
            if (Front != other.Front)
            {
                return Front.CompareTo(other.Front);
            }
            if (End != other.End)
            {
                return End.CompareTo(other.End);
            }
            return Content.CompareTo(other.Content);
        }

        public static implicit operator string (Car car)
        {
            return car.Content;
        }

        public static explicit operator Car(string s)
        {
            return new Car(s, s[0], s[s.Length - 1]);
        }
    }

    private string reverse(string s)
    {
        string result = string.Empty;
        for (int i = s.Length - 1; i >= 0; --i)
        {
            result += s[i];
        }
        return result;
    }

    public string hookUp(string[] cars)
    {
        int n = cars.Length;
        Car[] items = new Car[n];
        for (int i = 0; i < n; ++i)
        {
            string content = cars[i];
            string rcontent = reverse(cars[i]);
            if (rcontent.CompareTo(content) < 0)
            {
                content = rcontent;
            }
            items[i] = (Car)content;
        }

        Array.Sort(items);
        
        int[] dp = new int[n];
        Car[] train = new Car[n];
        int max = 0;
        for (int i = 0; i < n; ++i)
        {
            dp[i] = 1;
            train[i] = (Car)items[i];
            for (int j = 0; j < i; ++j)
            {
                if (train[j].End == items[i].Front)
                {
                    string car = train[j] + '-' + items[i];
                    if (dp[i] < dp[j] + 1 || (dp[i] == dp[j] + 1 && car.CompareTo(train[i]) < 0))
                    {
                        dp[i] = dp[j] + 1;
                        train[i] = (Car)car;
                    }
                }
                if (dp[i] == dp[j] && train[i].End == train[j].End)
                {
                    if (train[j].CompareTo(train[i]) < 0)
                    {
                        train[i] = train[j];
                    }
                }
            }
            max = Math.Max(max, dp[i]);
        }
        string result = string.Empty;
        for (int i = 0; i < n; ++i)
        {
            if (dp[i] == max && (string.IsNullOrEmpty(result) || result.CompareTo(train[i]) > 0))
            {
                result = train[i];
            }
        }
        return result;
    }


    // BEGIN CUT HERE
    public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); }
	private void verify_case(int Case, string Expected, string Received) {
		Console.Write("Test Case #" + Case + "...");
		if (Expected == Received) 
			Console.WriteLine("PASSED"); 
		else { 
			Console.WriteLine("FAILED"); 
			Console.WriteLine("\tExpected: \"" + Expected + '\"');
			Console.WriteLine("\tReceived: \"" + Received + '\"'); } }
	private void test_case_0() { string[] Arg0 = new string[]{"CBA","DAA","CXX"}; string Arg1 = "ABC-CXX"; verify_case(0, Arg1, hookUp(Arg0)); }
	private void test_case_1() { string[] Arg0 = new string[]{"ACBA"}; string Arg1 = "ABCA"; verify_case(1, Arg1, hookUp(Arg0)); }
	private void test_case_2() { string[] Arg0 = new string[]{"AUTOMATA","COMPUTER","ROBOT"}; string Arg1 = "COMPUTER-ROBOT"; verify_case(2, Arg1, hookUp(Arg0)); }
	private void test_case_3() { string[] Arg0 = new string[]{"AAA","AAAA","AAA","AAA"}; string Arg1 = "AAA-AAA-AAA-AAAA"; verify_case(3, Arg1, hookUp(Arg0)); }
	private void test_case_4() { string[] Arg0 = new string[]{"ABA","BBB","COP","COD","BAD"}; string Arg1 = "BBB-BAD"; verify_case(4, Arg1, hookUp(Arg0)); }

// END CUT HERE


    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        WordTrain item = new WordTrain();
        item.run_test(-1);
        Console.WriteLine(item.hookUp(new string[] { "ABABABAB", "BABABABA", "ABAB", "BABA", "ABABABAB", "BABABABA", "ABABABABA", "BABABABAB",
            "ABABABAB", "BABABABA", "ABABABAB", "BABABABA", "ABAB", "BABA", "ABABABAB", "BABABABA", "ABABABABA", "BABABABAB", "ABABABAB", "BABABABA",
            "ABABABAB", "BABABABA", "ABAB", "BABA", "ABABABAB", "BABABABA",
            "ABABABABA", "BABABABAB", "ABABABAB", "BABABABA", "ABABABAB", "BABABABA", "ABAB", "BABA",
            "ABABABAB", "BABABABA", "ABABABABA", "BABABABAB", "ABABABAB", "BABABABA", "ABABABAB", "BABABABA", "ABAB",
            "BABA", "ABABABAB", "BABABABA", "ABABABABA", "BABABABAB", "ABABABAB", "BABABABA" }));
        Console.ReadLine();
    }
    // END CUT HERE
}

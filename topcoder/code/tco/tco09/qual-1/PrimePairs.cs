using System;
using System.Collections.Generic;

public class PrimePairs
{
    private bool is_prime(int x)
    {
        for (int i = 2; i * i <= x; ++i)
        {
            if (x % i == 0)
            {
                return false;
            }
        }
        return true;
    }

    public int[] matches(int[] numbers)
    {
        int n = numbers.Length;
        bool[,] g = new bool[n, n];
        for (int i = 0; i < n; ++i)
        {
            for (int j = i + 1; j < n; ++j)
            {
                g[i, j] = is_prime(numbers[i] + numbers[j]);
                g[j, i] = g[i, j];
            }
        }
        List<int> result = new List<int>();
        for (int i = 1; i < n; ++i)
        {
            if (g[0, i])
            {
                int[,] capacity = new int[2 * n + 2, 2 * n + 2];
                for (int j = 1; j < n; ++j)
                {
                    if (j != i)
                    {
                        capacity[0, j] = 1;
                        capacity[n + j, 2 * n + 1] = 1;
                    }
                }
                for (int j = 1; j < n; ++j)
                {
                    if (j != i)
                    {
                        for (int k = 1; k < n; ++k)
                        {
                            if (k != i && k != j)
                            {
                                if (g[j, k])
                                {
                                    capacity[j, n + k] = 1;
                                }
                            }
                        }
                    }
                }
                if (get_flow(capacity, new int[2 * n + 2, 2 * n + 2], 2 * n + 2, 0, 2 * n + 1) == n - 2)
                {
                    result.Add(numbers[i]);
                }
            }
        }
        result.Sort();
        return result.ToArray();
    }

    private int get_flow(int[,] capacity, int[,] flow, int n, int source, int destination)
    {
        int result = 0;
        while (augment(capacity, flow, n, destination, source, new bool[n]))
        {
            ++result;
        }
        return result;
    }

    private bool augment(int[,] capacity, int[,] flow, int n, int destination, int current, bool[] visited)
    {
        if (visited[current])
        {
            return false;
        }
        if (current == destination)
        {
            return true;
        }
        visited[current] = true;
        for (int i = 0; i < n; ++i)
        {
            if (flow[current, i] < capacity[current, i])
            {
                if (augment(capacity, flow, n, destination, i, visited))
                {
                    ++flow[current, i];
                    --flow[i, current];
                    return true;
                }
            }
        }
        return false;
    }

    // BEGIN CUT HERE
    public void run_test(int Case)
    {
        if ((Case == -1) || (Case == 0)) test_case_0();
        if ((Case == -1) || (Case == 1)) test_case_1();
        if ((Case == -1) || (Case == 2)) test_case_2();
        if ((Case == -1) || (Case == 3)) test_case_3();
        if ((Case == -1) || (Case == 4)) test_case_4();
        if ((Case == -1) || (Case == 5)) test_case_5();
    }
    private void verify_case(int Case, int[] Expected, int[] Received)
    {
        Console.Write("Test Case #" + Case + "...");
        if (equal_arrays(Expected, Received))
            Console.WriteLine("PASSED");
        else
        {
            Console.WriteLine("FAILED");
            Console.WriteLine("\tExpected: " + print_array(Expected));
            Console.WriteLine("\tReceived: " + print_array(Received));
        }
    }
    string print_array(int[] V)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append("{ ");
        foreach (int o in V)
        {
            builder.Append('\"');
            builder.Append(o.ToString());
            builder.Append("\",");
        }
        builder.Append(" }");
        return builder.ToString();
    }
    bool equal_arrays(int[] a, int[] b)
    {
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; ++i) if (a[i] != b[i]) return false;
        return true;
    }
    private void test_case_0() { int[] Arg0 = new int[] { 1, 4, 7, 10, 11, 12 }; int[] Arg1 = new int[] { 4, 10 }; verify_case(0, Arg1, matches(Arg0)); }
    private void test_case_1() { int[] Arg0 = new int[] { 11, 1, 4, 7, 10, 12 }; int[] Arg1 = new int[] { 12 }; verify_case(1, Arg1, matches(Arg0)); }
    private void test_case_2() { int[] Arg0 = new int[] { 8, 9, 1, 14 }; int[] Arg1 = new int[] { }; verify_case(2, Arg1, matches(Arg0)); }
    private void test_case_3() { int[] Arg0 = new int[] { 34, 39, 32, 4, 9, 35, 14, 17 }; int[] Arg1 = new int[] { 9, 39 }; verify_case(3, Arg1, matches(Arg0)); }
    private void test_case_4()
    {
        int[] Arg0 = new int[]{ 941, 902, 873, 841, 948, 851, 945, 854, 815, 898,
  806, 826, 976, 878, 861, 919, 926, 901, 875, 864 }
; int[] Arg1 = new int[] { 806, 926 }; verify_case(4, Arg1, matches(Arg0));
    }

    private void test_case_5()
    {
        int[] Arg0 = new int[]{618, 263, 873, 514, 733, 94, 664, 307, 573, 900, 258, 11, 436, 366, 120, 
    738, 335, 270, 549, 393, 308, 648, 163, 190, 846, 209, 695, 816, 612, 422, 219, 675, 327, 702, 645, 513, 621, 568, 523, 516, 
    229, 390, 935, 658, 628, 141, 745, 92, 767, 465};
        int[] Arg1 = new int[] { }; verify_case(5, Arg1, matches(Arg0));
    }

    // END CUT HERE

    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        PrimePairs item = new PrimePairs();
        item.run_test(-1);
        Console.ReadLine();
    }
    // END CUT HERE
}

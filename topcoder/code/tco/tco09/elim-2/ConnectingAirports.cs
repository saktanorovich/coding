using System;

public class ConnectingAirports
{
    public string[] getSchedule(int[] ca, int[] cu)
    {
        if (get_total(ca) != get_total(cu))
        {
            return new string[0];
        }
        int n = ca.Length + cu.Length + 2;
        int[,] capacity = new int[n, n];
        int[,] flow = new int[n, n];
        for (int i = 0; i < ca.Length; ++i)
        {
            for (int j = 0; j < cu.Length; ++j)
            {
                capacity[i + 1, ca.Length + 1 + j] = 1;
            }
        }
        for (int i = 0; i < ca.Length; ++i)
        {
            capacity[0, i + 1] = ca[i];
        }
        for (int i = 0; i < cu.Length; ++i)
        {
            capacity[ca.Length + 1 + i, n - 1] = cu[i];
        }
        if (get_flow(capacity, flow, n, 0, n - 1) == get_total(ca))
        {
            string[] result = new string[ca.Length];
            for (int i = 0; i < ca.Length; ++i)
            {
                for (int j = 0; j < cu.Length; ++j)
                {
                    if (flow[i + 1, ca.Length + 1 + j] == 0)
                    {
                        result[i] += '0';
                        capacity[i + 1, ca.Length + 1 + j] = 0;
                    }
                    else
                    {
                        --flow[i + 1, ca.Length + 1 + j];
                        ++flow[ca.Length + 1 + j, i + 1];
                        --flow[0, i + 1];
                        ++flow[i + 1, 0];
                        --flow[ca.Length + 1 + j, n - 1];
                        ++flow[n - 1, ca.Length + 1 + j];
                        capacity[i + 1, ca.Length + 1 + j] = 0;
                        if (get_flow(capacity, flow, n, 0, n - 1) > 0)
                        {
                            result[i] += '0';
                        }
                        else
                        {
                            result[i] += '1';
                            capacity[i + 1, ca.Length + 1 + j] = 1;
                            get_flow(capacity, flow, n, 0, n - 1);
                        }
                    }
                }
            }
            return result;
        }
        return new string[0];
    }

    private int get_total(int[] capacity)
    {
        int result = 0;
        for (int i = 0; i < capacity.Length; ++i)
        {
            result += capacity[i];
        }
        return result;
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
    public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); if ((Case == -1) || (Case == 4)) test_case_4(); if ((Case == -1) || (Case == 5)) test_case_5(); }
    private void verify_case(int Case, string[] Expected, string[] Received)
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
    string print_array(string[] V)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append("{ ");
        foreach (string o in V)
        {
            builder.Append('\"');
            builder.Append(o.ToString());
            builder.Append("\",");
        }
        builder.Append(" }");
        return builder.ToString();
    }
    bool equal_arrays(string[] a, string[] b)
    {
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; ++i) if (a[i] != b[i]) return false;
        return true;
    }
    private void test_case_0() { int[] Arg0 = new int[] { 1, 2, 3 }; int[] Arg1 = new int[] { 3, 1, 2 }; string[] Arg2 = new string[] { "100", "101", "111" }; verify_case(0, Arg2, getSchedule(Arg0, Arg1)); }
    private void test_case_1() { int[] Arg0 = new int[] { 3, 2, 1, 1 }; int[] Arg1 = new int[] { 1, 3, 1, 2 }; string[] Arg2 = new string[] { "0111", "0101", "0100", "1000" }; verify_case(1, Arg2, getSchedule(Arg0, Arg1)); }
    private void test_case_2() { int[] Arg0 = new int[] { 1, 2, 3, 4 }; int[] Arg1 = new int[] { 5, 6, 7, 8 }; string[] Arg2 = new string[] { }; verify_case(2, Arg2, getSchedule(Arg0, Arg1)); }
    private void test_case_3() { int[] Arg0 = new int[] { 47, 47 }; int[] Arg1 = new int[] { 47, 40, 7 }; string[] Arg2 = new string[] { }; verify_case(3, Arg2, getSchedule(Arg0, Arg1)); }
    private void test_case_4() { int[] Arg0 = new int[] { 5, 5 }; int[] Arg1 = new int[] { 1, 1, 2, 1, 1, 1, 1, 1, 1 }; string[] Arg2 = new string[] { "001001111", "111110000" }; verify_case(4, Arg2, getSchedule(Arg0, Arg1)); }
    private void test_case_5() { int[] Arg0 = new int[] { 0, 0, 0, 0 }; int[] Arg1 = new int[] { 0, 0, 0, 0, 0, 0 }; string[] Arg2 = new string[] { "000000", "000000", "000000", "000000" }; verify_case(5, Arg2, getSchedule(Arg0, Arg1)); }

    // END CUT HERE

    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        ConnectingAirports item = new ConnectingAirports();
        item.run_test(-1);
        Console.ReadLine();
    }
    // END CUT HERE

}


// Powered by FileEdit
// Powered by TZTester 1.01 [25-Feb-2003] [modified for C# by Petr]
// Powered by CodeProcessor

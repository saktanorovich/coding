using System;
 
public class SortingWithPermutation
{
	public int[] getPermutation(int[] a)
	{
        int[] b = (int[])a.Clone();
        Array.Sort(b);
        bool[] used = new bool[a.Length];
        int[] result = new int[a.Length];
        for (int i = 0; i < a.Length; ++i)
        {
            for (int j = 0; j < a.Length; ++j)
            {
                if (b[i] == a[j] && !used[j])
                {
                    used[j] = true;
                    result[j] = i;
                    break;
                }
            }
        }
        return result;
	}
	
// BEGIN CUT HERE
	public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); }
	private void verify_case(int Case, int[] Expected, int[] Received) {
		Console.Write("Test Case #" + Case + "...");
		if (equal_arrays(Expected, Received)) 
			Console.WriteLine("PASSED"); 
		else { 
			Console.WriteLine("FAILED"); 
			Console.WriteLine("\tExpected: " + print_array(Expected)); 
			Console.WriteLine("\tReceived: " + print_array(Received)); } }
	string print_array(int[] V) {
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		builder.Append("{ ");
		foreach (int o in V) {
			builder.Append('\"');
			builder.Append(o.ToString());
			builder.Append("\",");
		}
		builder.Append(" }");
		return builder.ToString();
	}
	bool equal_arrays(int[] a, int[]b) {
		if (a.Length != b.Length) return false;
		for (int i = 0; i < a.Length; ++i) if (a[i] != b[i]) return false;
		return true;
	}
	private void test_case_0() { int[] Arg0 = new int[]{2, 3, 1}; int[] Arg1 = new int[]{1, 2, 0 }; verify_case(0, Arg1, getPermutation(Arg0)); }
	private void test_case_1() { int[] Arg0 = new int[]{2, 1, 3, 1}; int[] Arg1 = new int[]{2, 0, 3, 1 }; verify_case(1, Arg1, getPermutation(Arg0)); }
	private void test_case_2() { int[] Arg0 = new int[]{4, 1, 6, 1, 3, 6, 1, 4}; int[] Arg1 = new int[]{4, 0, 6, 1, 3, 7, 2, 5 }; verify_case(2, Arg1, getPermutation(Arg0)); }

// END CUT HERE

	// BEGIN CUT HERE
	[STAThread]
	public static void Main(string[] args)
	{
		SortingWithPermutation item = new SortingWithPermutation();
		item.run_test(-1);
		Console.ReadLine();
	}
	// END CUT HERE
}

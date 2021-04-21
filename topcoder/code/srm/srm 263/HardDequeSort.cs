using System;
using System.Collections.Generic;

public class HardDequeSort
{
    public class Item : IComparable<Item>
    {
        public int Element;
        public int Position;

        public Item(int element, int position)
        {
            Element = element;
            Position = position;
        }

        #region IComparable<Item> Members

        public int CompareTo(Item other)
        {
            if (Element != other.Element)
            {
                return Math.Sign(Element - other.Element);
            }
            else if (Position != other.Position)
            {
                return Math.Sign(Position - other.Position);
            }
            return 0;
        }

        #endregion
    }

    public int minDeques(int[] data)
    {
        int nitems = data.Length;
        Item[] items = new Item[nitems];
        for (int i = 0; i < nitems; ++i)
        {
            items[i] = new Item(data[i], i);
        }
        Array.Sort(items);

        /* greedy strategy */
        bool[] is_processed = new bool[nitems];
        for (int i = 0; i < nitems; ++i)
        {
            is_processed[items[i].Position] = true;
            if (!is_placeable(data, is_processed))
            {
                is_processed[items[i].Position] = false;
                break;
            }
        }

        List<int> unprocessed_data = new List<int>();
        for (int i = 0; i < nitems; ++i)
        {
            if (!is_processed[i])
            {
                unprocessed_data.Add(data[i]);
            }
        }
        return 1 + (unprocessed_data.Count > 0 ? minDeques(unprocessed_data.ToArray()) : 0);
    }

    private bool is_placeable(int[] data, bool[] taken)
    {
        List<int> deque = new List<int>();
        for (int i = 0; i < data.Length; ++i)
        {
            if (taken[i])
            {
                if (deque.Count == 0)
                {
                    deque.Add(data[i]);
                }
                else if (data[i] <= deque[0])
                {
                    deque.Insert(0, data[i]);
                }
                else if (data[i] >= deque[deque.Count - 1])
                {
                    deque.Add(data[i]);
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }

    // BEGIN CUT HERE
    public void run_test(int Case) { if ((Case == -1) || (Case == 0)) test_case_0(); if ((Case == -1) || (Case == 1)) test_case_1(); if ((Case == -1) || (Case == 2)) test_case_2(); if ((Case == -1) || (Case == 3)) test_case_3(); }
    private void verify_case(int Case, int Expected, int Received)
    {
        Console.Write("Test Case #" + Case + "...");
        if (Expected == Received)
            Console.WriteLine("PASSED");
        else
        {
            Console.WriteLine("FAILED");
            Console.WriteLine("\tExpected: \"" + Expected + '\"');
            Console.WriteLine("\tReceived: \"" + Received + '\"');
        }
    }
    private void test_case_0()
    {
        int[] Arg0 = new int[]{50, 45, 55, 60, 45, 65,
 40, 70, 70, 35, 30, 75}; int Arg1 = 1; verify_case(0, Arg1, minDeques(Arg0));
    }
    private void test_case_1() { int[] Arg0 = new int[] { 3, 6, 0, 9, 6, 3 }; int Arg1 = 2; verify_case(1, Arg1, minDeques(Arg0)); }
    private void test_case_2() { int[] Arg0 = new int[] { 0, 2, 1, 4, 3, 6, 5, 8, 7, 9 }; int Arg1 = 5; verify_case(2, Arg1, minDeques(Arg0)); }
    private void test_case_3() { int[] Arg0 = new int[] { 454, 537, 7, 976, 537, 908, 976, 908, -94, 454, 908, 64, 454, -731, 908, -646, 537 }; int Arg1 = 4; verify_case(3, Arg1, minDeques(Arg0)); }

    // END CUT HERE


    // BEGIN CUT HERE
    [STAThread]
    public static void Main(string[] args)
    {
        HardDequeSort item = new HardDequeSort();
        item.run_test(-1);
        Console.WriteLine(item.minDeques(new int[] { 1, 3, 2, 3, 2, 3 }));
        Console.ReadLine();
    }
    // END CUT HERE
}

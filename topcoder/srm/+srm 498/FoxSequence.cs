using System;
using System.Collections.Generic;

public class FoxSequence
{
      public struct Range
      {
            public readonly int lo, hi;
            public readonly int value;

            public Range(int lo, int hi, int value)
            {
                  this.lo = lo;
                  this.hi = hi;
                  this.value = value;
            }
      }

      public string isValid(int[] seq)
      {
            if (seq.Length > 4)
            {
                  int[] diff = new int[seq.Length - 1];
                  for (int i = 0; i < diff.Length; ++i)
                        diff[i] = seq[i + 1] - seq[i];
                  List<Range> ranges = new List<Range>();
                  int lo = 0;
                  for (int i = 1; i < diff.Length; ++i)
                        if (diff[i] != diff[i - 1])
                        {
                              ranges.Add(new Range(lo, i - 1, diff[lo]));
                              lo = i;
                        }
                  ranges.Add(new Range(lo, diff.Length - 1, diff[lo]));
                  if (3 < ranges.Count && ranges.Count < 6)
                  {
                        bool result = false;
                        if (ranges.Count > 4)
                              result = ranges[0].value > 0 && ranges[1].value < 0 && ranges[2].value == 0 && ranges[3].value > 0 && ranges[4].value < 0;
                        else
                              result = ranges[0].value > 0 && ranges[1].value < 0 && ranges[2].value > 0 && ranges[3].value < 0;
                        if (result)
                              return "YES";
                  }
            }
            return "NO";
      }

      static void Main(string[] args)
      {
            Console.WriteLine(new FoxSequence().isValid(new int[] { 1, 3, 5, 7, 5, 3, 1, 1, 1, 3, 5, 7, 5, 3, 1 }));
            Console.WriteLine(new FoxSequence().isValid(new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 2, 2, 3, 4, 5, 6, 4 }));
            Console.WriteLine(new FoxSequence().isValid(new int[] { 3, 6, 9, 1, 9, 5, 1 }));
            Console.WriteLine(new FoxSequence().isValid(new int[] { 1, 2, 3, 2, 1, 2, 3, 2, 1, 2, 3, 2, 1 }));
            Console.WriteLine(new FoxSequence().isValid(new int[] { 1, 3, 4, 3, 1, 1, 1, 1, 3, 4, 3, 1 }));
            Console.WriteLine(new FoxSequence().isValid(new int[] { 6, 1, 6 }));
            Console.WriteLine(new FoxSequence().isValid(new int[] { 460, 491, 522, 553, 584, 615, 646, 677, 708, 739, 770, 801, 832, 863, 894, 925, 956, 987, 1018, 1049, 1080, 1111, 1142, 1173, 1204, 1235, 1266, 1297, 1328, 1301, 1274, 1247, 1220, 1193, 1166, 1139, 1112, 1085, 1058, 1031, 1004, 977, 950, 923, 896, 869, 842, 815, 788, 761 }));
            Console.WriteLine(new FoxSequence().isValid(new int[] { 923, 969, 1015, 1061, 1107, 1153, 1199, 1245, 1291, 1337, 1288, 1239, 1190, 1141, 1092, 1043, 994, 945, 896, 847, 847, 847, 847, 900, 953, 1006, 1059, 1112, 1165, 1218, 1271, 1324, 1377, 1430, 1483, 1536, 1589, 1541, 1493, 1445, 1397, 1349, 1301, 1253, 1205, 1157, 1109, 1061, 1013, 12 }));

            Console.ReadLine();
      }
}


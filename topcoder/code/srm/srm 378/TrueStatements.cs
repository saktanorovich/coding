using System;
using System.Collections.Generic;

    class TrueStatements
    {
        public int numberTrue(int[] statements)
        {
            int n = statements.Length;
            int[] howMany = new int[51];
            for (int i = 0; i < n; ++i)
            {
                howMany[statements[i]]++;
            }
            for (int i = 50; i >= 1; --i)
            {
                if (howMany[i] == i)
                {
                    return i;
                }
            }
            return (howMany[0] == 0 ? 0 : -1);
        }
    }

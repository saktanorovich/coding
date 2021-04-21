using System;
using System.Collections.Generic;

    class LameKnight
    {
        public int maxCells(int height, int width)
        {
            if (height == 1 || width == 1)
            {
                return 1;
            }
            if (height == 2)
            {
                return Math.Min(4, width / 2 + width % 2);
            }
            if (height > 2 && width > 6)
            {
                return width - 2;
            }
            return Math.Min(width, 4);
        }
        static void Main(string[] args)
        {
        }
    }


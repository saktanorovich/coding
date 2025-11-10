using System;

public class BiggestRectangleHard
{
	int n, size;
	int[] sets;
	bool[] can;

	int next(int small, int big) 
	{
		return (small - 1) & big;
	}
	public int findArea(int[] lengths)
	{
		int maxArea = 0;
		n = lengths.Length;
		size = 1 << n;
		sets = new int[size]; 
		can = new bool[size];

		for (int mask = 1; mask < size; ++ mask)
		{
			sets[mask] = 0;
			for (int i = 0; i < n; ++ i)
				if ((mask & (1 << i)) != 0)
					sets[mask] += lengths[i];

			can[mask] = false;
			for (int x = mask; x > 0; x = next(x, mask))
				if (2 * sets[x] == sets[mask])
				{
					can[mask] = true;
					break;
				}
		}
		for (int mask = 1; mask < size; ++ mask)
			if (can[mask])
			{
				int a = sets[mask] / 2;
				int notMask = (size - 1) ^ mask;
				for (int x = notMask; x > 0; x = next(x, notMask))
					if (can[x])
					{
						int b = sets[x] / 2;
						if (a * b > maxArea)
							maxArea = a * b;
					}
			}
		return (maxArea == 0 ? -1 : maxArea);
	}
}
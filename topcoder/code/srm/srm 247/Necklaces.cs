using System;

public class Necklaces
{
	int m;
	int[] g, gsum;
	public int inequity(int n, int[] gems)
	{
		m = gems.Length;
		g = new int[m + 1];
		gsum = new int[m + 1];
		int best = Int32.MaxValue;

		for (int shift = 0; shift < m; ++ shift)
		{
			for (int i = 0; i < m; ++ i)
				g[i + 1] = gems[(i + shift) % m];

			gsum[0] = 0;
			for (int i = 1; i <= m; ++ i)
				gsum[i] = gsum[i - 1] + g[i];

			for (int p = 1; p <= m; ++ p)
			{
				int min = gsum[p];
				int[,] a = new int[m + 1, n + 1];
				a[p, 1] = min;

				for (int j = 2; j <= n; ++ j)
					for (int i = p + 1; i <= m; ++ i)
						for (int k = p + 1; k <= i; ++ k)
							if (gsum[i] - gsum[k - 1] >= min && a[k - 1, j - 1] >= min)
							{
								int temp = Math.Max(gsum[i] - gsum[k - 1], a[k - 1, j - 1]);
								if (a[i, j] == 0 || temp < a[i, j])
									a[i, j] = temp;
							}
				if (a[m, n] >= min)
					best = Math.Min(best, a[m, n] - min);
			}
		}
		return best;
	}
}
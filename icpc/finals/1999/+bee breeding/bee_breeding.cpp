#include <cstdio>

const int maxSize = 300;
const int maxAB = 10000;

struct tcell
{
	int i, j;
	tcell(int ii = 0, int jj = 0)
	{
		i = ii;
		j = jj;
	}
};

tcell coords[maxAB + 2];
tcell start, finish;
int aa, bb;

void fill(int bound)
{
	int i0 = maxSize / 2, j0 = maxSize / 2;
	coords[1] = tcell(i0, j0);
	coords[2] = tcell(i0 + 2, j0);
	int curI = i0 + 1, curJ = j0 - 1;
	int i = 2;
	while (true)
	{
		for (; curI + curJ != i0 + j0; -- curI, -- curJ)
		{
			coords[++ i] = tcell(curI, curJ);
			if (i >= bound)
				goto exit;
		}
		for (; curI - curJ != i0 - j0; curI -= 2)
		{
			coords[++ i] = tcell(curI, curJ);
			if (i >= bound)
				goto exit;
		}
		for (; curJ != j0; -- curI, ++ curJ)
		{
			coords[++ i] = tcell(curI, curJ);
			if (i >= bound)
				goto exit;
		}
		for (; curI + curJ != i0 + j0; ++ curI, ++ curJ)
		{
			coords[++ i] = tcell(curI, curJ);
			if (i >= bound)
				goto exit;
		}
		for (; curI - curJ != i0 - j0; curI += 2)
		{
			coords[++ i] = tcell(curI, curJ);
			if (i >= bound)
				goto exit;
		}
		coords[++ i] = tcell(curI, curJ);
		if (i >= bound)
			goto exit;
		curI += 2;
		for (; curJ != j0; ++ curI, -- curJ)
		{
			coords[++ i] = tcell(curI, curJ);
			if (i >= bound)
				goto exit;
		}
	}
exit:
	return;
}

int abs(int x) { return (x < 0) ? -x : x; }
int min(int a, int b) { return (a < b) ? a : b; }

int main()
{
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	fill(maxAB + 1);
	scanf("%d%d", &aa, &bb);
	while (aa > 0 && bb > 0)
	{
		start = coords[aa];
		finish = coords[bb];
		
		int dist;
		if (start.j == finish.j)
			dist = abs(start.i - finish.i) / 2;
		else 
			if (start.i + start.j == finish.i + finish.j || start.i - start.j == finish.i - finish.j)
				dist = abs(start.j - finish.j);
			else
				if (start.i == finish.i)
					dist = abs(start.j - finish.j);
				else
				{
					{
						int j3, j4;
						j3 = (start.i + start.j - (finish.i - finish.j)) / 2;
						j4 = (finish.i + finish.j - (start.i - start.j)) / 2;
						dist = min(abs(finish.j - j3) + abs(start.j - j3), abs(finish.j - j4) + abs(start.j - j4));
					}
					{
						int i3, j3;
						i3 = finish.i + finish.j - start.j;
						j3 = start.j;
						dist = min(dist, abs(finish.j - j3) + abs(start.i - i3) / 2);
						
						int i4, j4;
						i4 = start.i;
						j4 = start.i - finish.i + finish.j;
						dist = min(dist, abs(finish.j - j4) + abs(start.j - j4));

						int i5, j5;
						i5 = finish.i - finish.j + start.j;
						j5 = start.j;
						dist = min(dist, abs(finish.j - j5) + abs(start.i - i5) / 2);

						int i6, j6;
						i6 = start.i;
						j6 = finish.i + finish.j - start.i;
						dist = min(dist, abs(finish.j - j6) + abs(start.j - j6));
					}
					{
						int i3, j3;
						i3 = start.i + start.j - finish.j;
						j3 = finish.j;
						dist = min(dist, abs(start.j - j3) + abs(finish.i - i3) / 2);
						
						int i4, j4;
						i4 = finish.i;
						j4 = finish.i - start.i + start.j;
						dist = min(dist, abs(start.j - j4) + abs(finish.j - j4));

						int i5, j5;
						i5 = start.i - start.j + finish.j;
						j5 = finish.j;
						dist = min(dist, abs(start.j - j5) + abs(finish.i - i5) / 2);

						int i6, j6;
						i6 = finish.i;
						j6 = start.i + start.j - finish.i;
						dist = min(dist, abs(start.j - j6) + abs(finish.j - j6));
					}
				}
		printf("The distance between cells %d and %d is %d.\n", aa, bb, dist);
		scanf("%d%d", &aa, &bb);
	}
	return 0;
}

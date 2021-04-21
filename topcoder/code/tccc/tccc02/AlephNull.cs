using System;

	class AlephNull
	{
		public int[] rational(int generation, int item)
		{
			if (item >= (1 << (generation - 1)) + 1)
			{
				return new int[]{0, 0};
			}
			if (generation == 1)
			{
				return (item == 0 ? new int[]{0, 1} : new int[]{1, 0});
			}
			if (item % 2 == 0)
			{
				return rational(generation - 1, item / 2);
			}
			int[] r1 = rational(generation - 1, (item - 1) / 2);
			int[] r2 = rational(generation - 1, (item + 1) / 2);
			return new int[]{r1[0] + r2[0], r1[1] + r2[1]};
		}
		static void Main(string[] args)
		{
			AlephNull an = new AlephNull();
			int[] result;
			result = an.rational(1, 0);//  {0, 1}
			result = an.rational(1, 1);//  {1, 0}
			result = an.rational(1, 2);//  {0, 0}
			result = an.rational(4, 1);//  {1, 3}
			result = an.rational(4, 6);//  {2, 1}
			result = an.rational(5, 2);//  {1, 3}
			result = an.rational(5, 11);//  {5, 3}
			result = an.rational(5, 16);//  {1, 0}
			result = an.rational(5, 17);//  {0, 0}
			result = an.rational(8, 70);//  {9, 7}
			result = an.rational(10, 467);//  {43, 12}
			result = an.rational(23, 4190316);//  {438, 43}
			result = an.rational(30, 100);//  {7, 157}
			result = an.rational(30, 536866925);//  {1753, 102}
			result = an.rational(30, 536870912);//  {1, 0}
			result = an.rational(30, 536870913);//  {0, 0}  
		}
	}

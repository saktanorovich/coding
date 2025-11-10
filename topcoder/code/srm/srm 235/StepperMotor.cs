using System;

class StepperMotor
{
	private long steps(long n, long from, long to)
	{
        long s1 = (to % n - from % n - n) % n;
        long s2 = (to % n - from % n + n) % n;
        long s3 = (to % n - from % n - 2 * n) % n;
        long s4 = (to % n - from % n + 2 * n) % n;
        long result = s1;
        if (Math.Abs(s2) < Math.Abs(result) || (Math.Abs(s2) == Math.Abs(result) && s2 > 0))
        {
            result = s2;
        }
        if (Math.Abs(s3) < Math.Abs(result) || (Math.Abs(s3) == Math.Abs(result) && s3 > 0))
        {
            result = s3;
        }
        if (Math.Abs(s4) < Math.Abs(result) || (Math.Abs(s4) == Math.Abs(result) && s4 > 0))
        {
            result = s4;
        }
        return result;
	}
	public int rotateToNearest(int n, int current, int[] target)
	{
		long ans = long.MaxValue;
		for (int i = 0; i < target.Length; ++ i)
		{
			long s = steps(n, current, target[i]);
			if (Math.Abs(ans) > Math.Abs(s) || (Math.Abs(ans) == Math.Abs(s) && s > ans))
			{
				ans = s;
			}
		}
		return (int)ans;
	}
}

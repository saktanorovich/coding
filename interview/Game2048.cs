public class Game2048
{
	public int[,] Swipe(int[,] board, int direction)
	{
		var rotated = Rotate(board, direction);
		var current = Swipe(rotated);
		var answer  = Rotate(current, 4 - direction);
		return answer;
	}

	private int[,] Swipe(int[,] board)
	{
		var res = new int[4, 4];
		for (var i = 0; i < 4; ++i)
		{
			for (int j = 3, p = 3; j >= 0; --j)
			{
				if (board[i, j] > 0)
				{
					var state = res[i, p];
					res[i, p] = board[i, j];
					if (state == -1)
					{
						continue;
					}
					p = p - 1;
					if (p < 3)
					{
						if (res[i, p] == res[i, p + 1])
						{
							res[i, p + 1] *= 2;
							res[i, p] = -1;
						}
					}
				}
			}
		}
		return res;
	}

	private int[,] Rotate(int[,] board, int times)
	{
		for (var i = 0; i < times; ++i)
		{
			board = Rotate(board);
		}
		return board;
	}

	// Rotate board 90 degree clockwise
	private int[,] Rotate(int[,] board)
	{
		var res = new int[4, 4];
		for (var i = 0; i < 4; ++i)
		{
			for (var j = 3; j >= 0; --j)
			{
				res[i, 3 - j] = board[j, i];
			}
		}
		return res;
	}
}

#include <iostream>
#include <vector>

using namespace std;

int const max_width = 80;
char const split[max_width + 2] = "--------------------------------------------------------------------------------\n";
int const max_nrows = 4;
int const max_ncolumns = 800;
char const terminal = '#';

typedef unsigned long long uint64;

class ttyper
{
private:
	int _row, _column;
	char _buffer[max_nrows][max_ncolumns];
	bool _is_row_empty[max_nrows];
private:
	bool is_blank_char(char const &c)
	{
		return (c == terminal || c == ' ');
	}
	void to_output(int row, int col1, int col2)
	{
		for (int j(col1); j <= col2; ++j)
		{
			cout << (_buffer[row][j] == terminal ? ' ' : _buffer[row][j]);
		}
	}
	void to_output(int col1, int col2, int height, int indent)
	{
		for (int i(height); i >= 0; --i)
		{
			bool is_empty = true;
			for (int j(col1); j <= col2; ++j)
			{
				if (!is_blank_char(_buffer[i][j]))
				{
					is_empty = false;
					break;
				}
			}
			if (!is_empty)
			{
				for (int j(0); j < indent; ++j)
				{
					cout << ' ';
				}
				int col(col2);
				for (; is_blank_char(_buffer[i][col]); --col);
				to_output(i, col1, col);
			}
			cout << endl;
		}
	}
public:
	ttyper()
	{
	}
	void put(char const &c)
	{
		_buffer[_row][_column] = c;
		_is_row_empty[_row] = is_blank_char(_buffer[_row][_column]);
		++_column;
	}
	void put(string const &s)
	{
		for (int i(0); i < s.length(); ++i)
		{
			put(s[i]);
		}
	}
	void put(uint64 const &n)
	{
		vector<int> digits;
		for (uint64 x(n); x > 0; x /= 10)
		{
			digits.push_back(x % 10);
		}
		for (int i(digits.size() - 1); i >= 0; --i)
		{
			put((char)(digits[i] + 48));
		}
	}
	void set_row(int const &row)
	{
		_row = row;
	}
	int get_row()
	{
		return _row;
	}
	int get_column()
	{
		return _column;
	}
	void clear()
	{
		_row = 0;
		_column = 0;
		for (int i(0); i < max_nrows; ++i)
		{
			for (int j(0); j < max_ncolumns; ++j)
			{
				_buffer[i][j] = terminal;
			}
			_is_row_empty[i] = true;
		}
	}
	void to_output(int max_width, int indent)
	{
		int height = 0;
		for (int i(max_nrows - 1); i >= 0; --i)
		{
			if (!_is_row_empty[i])
			{
				height = i;
				break;
			}
		}
		int col1(0), col2(0), col, current_indent(0);
		bool is_first_block = true;
		for (col = 0; col < _column; ++col)
		{
			if (_buffer[0][col] == '+' || col == _column - 1)
			{
				int c = (col != _column - 1 ? col - 1 : col);
				if (c - col1 + 1 + current_indent > max_width)
				{
					if (is_first_block)
					{
						to_output(col1, col2, height, 0);
						is_first_block = false;
						current_indent = indent;
					}
					else
					{
						cout << endl;
						to_output(col1, col2, height, indent);
					}
					col1 = col2 + 1;
				}
				col2 = col - 1;
			}
		}
		if (is_first_block)
		{
			to_output(col1, _column - 1, height, 0);
		}
		else
		{
			cout << endl;
			to_output(col1, _column - 1, height, indent);
		}
	}
};

struct texpansion
{
	uint64 base;
	uint64 coefficient;
	texpansion *pow, *next;
	texpansion(uint64 const &base)
	{
		this->base = base;
		coefficient = 1;
		pow = next = 0;
	}
};

pair<uint64, uint64> get_pow_base(uint64 const &n, uint64 const &base)
{
	uint64 pow(0), pow_base(1);
	for (uint64 x(n); x >= base; x = x / base)
	{
		++pow;
		pow_base *= base;
	}
	return make_pair(pow, pow_base);
}

texpansion *get_complete_base_expansion(uint64 const &n, uint64 const &base)
{
	if (n > 0)
	{
		texpansion *result = new texpansion(base);
		if (n <= base)
		{
			result->base = n;
		}
		else
		{
			pair<uint64, uint64> pow_base_pair = get_pow_base(n, base);
			result->coefficient = n / pow_base_pair.second;
			if (pow_base_pair.first > 1)
			{
				result->pow = get_complete_base_expansion(pow_base_pair.first, base);
			}
			result->next = get_complete_base_expansion(n - result->coefficient * pow_base_pair.second, base);
		}
		return result;
	}
	return 0;
}

void print(texpansion *expansion, ttyper *typer)
{
	if (expansion->coefficient > 1)
	{
		typer->put(expansion->coefficient);
		typer->put('*');
	}
	typer->put(expansion->base);
	if (expansion->pow != 0)
	{
		typer->set_row(typer->get_row() + 1);
		print(expansion->pow, typer);
		typer->set_row(typer->get_row() - 1);
	}
	if (expansion->next != 0)
	{
		typer->put('+');
		print(expansion->next, typer);
	}
}

int main()
{
	uint64 n, base;
	cin >> n >> base;
	ttyper *typer = new ttyper();
	while (n > 0 && base > 1)
	{
		typer->clear();
		cout << n << " in complete base " << base << ":" << endl << endl;
		texpansion *expansion = get_complete_base_expansion(n, base);
		typer->put(n);
		typer->put(" = ");
		int indent = typer->get_column();
		print(expansion, typer);
		typer->to_output(max_width, indent);
		cin >> n >> base;
		if (n > 0 && base > 1)
		{
			cout << split;
		}
	}
	cout.flush();
	return 0;
}

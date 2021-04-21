#include <algorithm>
#include <cstdio>
#include <iostream>
#include <sstream>
#include <stack>
#include <string>
#include <vector>
using namespace std;

struct titem
{
	int element;
	int position;
	titem(int element, int position)
	{
		this->element = element;
		this->position = position;
	}
	bool operator <(titem const &item) const
	{
		if (element != item.element)
		{
			return element < item.element;
		}
		return position < item.position;
	}
};

istringstream *get_stream(istream *is)
{
	char buffer[2048];
	is->getline(buffer, 2048);
	return new istringstream(buffer);
}

bool target(string const &data, vector<bool> taken)
{
	stack<int> s;
	for (size_t i(0); i < data.length(); ++i)
	{
		if (taken[i])
		{
			int container = data[i] - 'A';
			if (s.size() == 0)
			{
				s.push(container);
			}
			else if (container <= s.top())
			{
				s.push(container);
			}
			else
			{
				return false;
			}
		}
	}
	return true;
}

int get_total_stacks(string const &data)
{
	int nitems = data.length();
	vector<titem> items;
	for (int i(0); i < nitems; ++i)
	{
		items.push_back(titem(data[i] - 'A', i));
	}
	sort(items.begin(), items.end());
	vector<bool> taken(nitems, false);
	for (int i(0); i < nitems; ++i)
	{
		taken[items[i].position] = true;
		if (!target(data, taken))
		{
			taken[items[i].position] = false;
		}
	}
	string unprocessed_data = "";
	for (int i(0); i < nitems; ++i)
	{
		if (!taken[i])
		{
			unprocessed_data += data[i];
		}
	}
	return 1 + (unprocessed_data.length() > 0 ? get_total_stacks(unprocessed_data) : 0);
}

int main()
{
	for (int test_case(1); ; ++test_case)
	{
		string data;
		istringstream *stream = get_stream(&cin);
		*stream >> data;
		if (data == "end")
		{
			break;
		}
		int total_stacks = get_total_stacks(data);
		cout << "Case " << test_case << ": " << total_stacks << endl;
	}
}

#include <bits/stdc++.h>
using namespace std;

string ltrim(const string &);
string rtrim(const string &);
vector<string> split(const string &);

int dx[4] = { -1,  0, +1,  0 };
int dy[4] = {  0, -1,  0, +1 };

int minimumMoves(vector<string> grid, int startX, int startY, int goalX, int goalY) {
    int const n = grid.size();
    vector<vector<int>> best(n, vector<int>(n, (int)1e6));
    best[startX][startY] = 0;
    queue<int> queue;
    queue.push(startX);
    queue.push(startY);
    while (!queue.empty()) {
        int cx = queue.front(); queue.pop();
        int cy = queue.front(); queue.pop();
        if (cx == goalX && cy == goalY) {
            return best[cx][cy];
        }
        for (int k = 0; k < 4; ++k) {
            int nx = cx;
            int ny = cy;
            while (1) {
                if (0 <= nx && nx < n && 0 <= ny && ny < n) {
                    if (grid[nx][ny] == 'X') {
                        break;
                    }
                    if (best[nx][ny] > best[cx][cy] + 1) {
                        best[nx][ny] = best[cx][cy] + 1;
                        queue.push(nx);
                        queue.push(ny);
                    }
                } else break;
                nx += dx[k];
                ny += dy[k];
            }
        }
    }
    return -1;
}

int main() {
    ofstream fout(getenv("OUTPUT_PATH"));
    string n_temp;
    getline(cin, n_temp);
    int n = stoi(ltrim(rtrim(n_temp)));
    vector<string> grid(n);
    for (int i = 0; i < n; i++) {
        string grid_item;
        getline(cin, grid_item);
        grid[i] = grid_item;
    }
    string first_multiple_input_temp;
    getline(cin, first_multiple_input_temp);
    vector<string> first_multiple_input = split(rtrim(first_multiple_input_temp));
    int startX = stoi(first_multiple_input[0]);
    int startY = stoi(first_multiple_input[1]);
    int goalX = stoi(first_multiple_input[2]);
    int goalY = stoi(first_multiple_input[3]);
    int result = minimumMoves(grid, startX, startY, goalX, goalY);
    fout << result << "\n";
    fout.close();
    return 0;
}

string ltrim(const string &str) {
    string s(str);
    s.erase(
        s.begin(),
        find_if(s.begin(), s.end(), not1(ptr_fun<int, int>(isspace)))
    );
    return s;
}

string rtrim(const string &str) {
    string s(str);
    s.erase(
        find_if(s.rbegin(), s.rend(), not1(ptr_fun<int, int>(isspace))).base(),
        s.end()
    );
    return s;
}

vector<string> split(const string &str) {
    vector<string> tokens;
    string::size_type start = 0;
    string::size_type end = 0;
    while ((end = str.find(" ", start)) != string::npos) {
        tokens.push_back(str.substr(start, end - start));
        start = end + 1;
    }
    tokens.push_back(str.substr(start));
    return tokens;
}


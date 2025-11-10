class Solution {
public:
    int trapRainWater(std::vector<std::vector<int>>& heightMap) {
        int n = heightMap.size();
        int m = heightMap[0].size();
        std::vector<std::vector<int>> a(n + 1, std::vector<int>(m + 1));
        std::vector<std::vector<int>> b(n + 1, std::vector<int>(m + 1));
        for (int i = 1; i <= n; ++i) {
            for (int j = 1; j <= m; ++j) {
                a[i][j] = heightMap[i - 1][j - 1];
                b[i][j] = (int)1e+6;
            }
        }
        std::queue<int> q;
        // boundary elements do not have any water
        for (int i = 1; i <= n; ++i) {
            b[i][1] = a[i][1];
            b[i][m] = a[i][m];
            q.push(i);
            q.push(1);
            q.push(i);
            q.push(m);
        }
        // boundary elements do not have any water
        for (int j = 1; j <= m; ++j) {
            b[1][j] = a[1][j];
            b[n][j] = a[n][j];
            q.push(1);
            q.push(j);
            q.push(n);
            q.push(j);
        }
        while (!q.empty()) {
            int x1 = q.front(); q.pop();
            int y1 = q.front(); q.pop();
            for (int k = 0; k < 4; ++k) {
                int x2 = x1 + dx[k];
                int y2 = y1 + dy[k];
                if (1 <= x2 && x2 <= n && 1 <= y2 && y2 <= m) {
                    // if we have a waterfall
                    if (b[x2][y2] > b[x1][y1]) {
                        b[x2][y2] = std::max(a[x2][y2], b[x1][y1]);
                        q.push(x2);
                        q.push(y2);
                    }
                }
            }
        }
        int res = 0;
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < m; ++j) {
                res += b[i][j] - a[i][j];
            }
        }
        return res;
    }

    int dx[4] = { -1, 0, +1, 0 };
    int dy[4] = { 0, -1, 0, +1 };
};

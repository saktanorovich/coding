class Solution {
    typedef long long i64;
private:
    i64 spfa(vector<vector<pair<int, int>>>& graph, size_t n, int c) {
        vector<i64> best(n, (i64)1e+18);
        best[0] = 0;
        vector<bool> has(n, false);
        queue<int> que;
        que.push(0);
        while (!que.empty()) {
            int curr = que.front(); que.pop();
            has[curr] = false;
            for (auto& e : graph[curr]) {
                int next = e.first;
                int cost = e.second;
                if (cost < c) {
                    continue;
                }
                if (best[next] > best[curr] + cost) {
                    best[next] = best[curr] + cost;
                    if (has[next] == false) {
                        has[next] = true;
                        que.push(next);
                    }
                }
            }
        }
        return best[n - 1];
    }
    int find(vector<vector<pair<int, int>>>& graph, size_t n, i64 k) {
        int lo = 0;
        int hi = (int)1e+9 + 1;
        while (lo < hi) {
            int cost = (lo + hi + 1) / 2;
            i64 dist = spfa(graph, n, cost);
            if (dist > k) {
                hi = cost - 1;
            } else {
                lo = cost;
            }
        }
        return spfa(graph, n, hi) <= k ? hi : -1;
    }
public:
    int findMaxPathScore(vector<vector<int>>& edges, vector<bool>& online, long long k) {
        size_t n = online.size();
        vector<vector<pair<int, int>>> graph(n, vector<pair<int, int>>());
        for (auto& e : edges) {
            int u = e[0];
            int v = e[1];
            int c = e[2];
            if (!online[u] || !online[v]) {
                continue;
            }
            graph[u].push_back({ v, c });
        }
        int answ = find(graph, n, k);
        return answ;
    }
};

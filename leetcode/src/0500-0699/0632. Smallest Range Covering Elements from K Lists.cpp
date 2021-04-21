class Solution {
public:
    vector<int> smallestRange(vector<vector<int>>& nums) {
        vector<int> uniq = unique(nums);
        unordered_map<int, int> ind;
        unordered_map<int, int> rev;
        int n = nums.size();
        int m = uniq.size();
        for (int i = 0; i < m; ++i) {
            ind[uniq[i]] = i;
            rev[i] = uniq[i];
        }
        segment_tree tree(m);
        for (int indx = 0; indx < n; ++indx) {
            for (int num : nums[indx]) {
                tree.update(ind[num], indx);
            }
        }
        int leRes = *uniq.cbegin();
        int riRes = *uniq.rbegin();
        for (int pt = 0; pt < m; ++pt) {
            int lo = pt;
            int hi = m - 1;
            while (lo < hi) {
                int mid = (lo + hi) / 2;
                int cnt = tree.query(pt, mid + 1);
                if (cnt < n) {
                    lo = mid + 1;
                } else {
                    hi = mid;
                }
            }
            if (tree.query(pt, lo + 1) == n) {
                int leCur = rev[pt];
                int riCur = rev[lo];
                if (riCur - leCur < riRes - leRes) {
                    leRes = leCur;
                    riRes = riCur;
                }
            }
        }
        return vector<int> { leRes, riRes };
    }

    vector<int> unique(vector<vector<int>>& nums) {
        set<int> unique;
        for (int i = 0; i < nums.size(); ++i) {
            for (int num : nums[i]) {
                unique.insert(num);
            }
        }
        return vector<int>(unique.begin(), unique.end());
    }
private:
    struct segment_tree {
        vector<bitset<3500>> tree;
        int n;

        segment_tree(int n) {
            this->tree = vector<bitset<3500>>(2 * n);
            this->n = n;
        }

        void update(int at, int index) {
            tree[at += n].set(index);
            for (; at > 1; at >>= 1) {
                tree[at >> 1].set(index);
            }
        }

        int query(int le, int ri) {
            bitset<3500> res;
            le += n;
            ri += n;
            for (; le < ri; le >>= 1, ri >>= 1) {
                if (le & 1) {
                    res |= tree[le++];
                }
                if (ri & 1) {
                    res |= tree[--ri];
                }
            }
            return res.count();
        }
    };
};
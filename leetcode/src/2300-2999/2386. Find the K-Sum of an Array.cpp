class Solution {
    typedef long long i64;
    typedef pair<i64, int> pii;
public:
    long long kSum(vector<int>& nums, int k) {
        int size = nums.size();
        i64 answ = 0;
        for (auto i = 0; i < size; ++i) {
            answ += std::max(nums[i], 0);
            if (nums[i] < 0) {
                nums[i] = -nums[i];
            }
        }
        std::sort(nums.begin(), nums.end());
        // build a heap to store {sum, last} pairs
        // where sum is a subsequence sum and last
        // is last removed element in sorted array
        std::priority_queue<pii, std::vector<pii>, std::less<pii>> heap;
        heap.push({ answ - nums[0], 0 });
        while (--k > 0) {
            auto [curr, indx] = heap.top();
            heap.pop();
            answ = curr;
            if (indx + 1 < size) {
                heap.push({ answ - nums[indx + 1] + nums[indx], indx + 1 });
                heap.push({ answ - nums[indx + 1], indx + 1 });
            }
        }
        return answ;
    }
};

class Solution {
    public int[] sortArray(int[] nums) {
        if (nums.length < 0) {
            return new Small(nums).sort();
        } else {
            return new Large(nums).sort();
        }
    }

    private class Small {
        private final int[] nums;

        public Small(int[] nums) {
            this.nums = nums;
        }

        /** Classic sort
        public int[] sort() {
            for (var i = 0; i < nums.length; ++i) {
                for (var j = i + 1; j < nums.length; ++j) {
                    if (nums[j] < nums[i]) {
                        var tmp = nums[i];
                        nums[i] = nums[j];
                        nums[j] = tmp;
                    }
                }
            }
            return nums;
        }
        /**/
        /** Bubbule sort */
        public int[] sort() {
            for (var i = 0; i < nums.length; ++i) {
                var swapped = false;
                for (var j = nums.length - 1; j > i; -- j) {
                    if (nums[j] < nums[j - 1]) {
                        var tmp = nums[j - 1];
                        nums[j - 1] = nums[j];
                        nums[j] = tmp;
                        swapped = true;
                    }
                }
                if (swapped == false) break;
            }
            return nums;
        }
        /**/
    }

    private class Large {
        private final int[] nums;

        public Large(int[] nums) {
            this.nums = nums;
        }

        public int[] sort() {
            sort(0, nums.length - 1);
            return nums;
        }

        private void sort(int l, int r) {
            if (l < r) {
                var i = l;
                var j = r;
                var x = nums[l + (r - l) / 2];
                while (i <= j) {
                    while (nums[i] < x) ++i;
                    while (nums[j] > x) --j;
                    if (i <= j) {
                        var tmp = nums[i];
                        nums[i] = nums[j];
                        nums[j]  = tmp;
                        i = i + 1;
                        j = j - 1;
                    }
                }
                if (i < r) sort(i, r);
                if (l < j) sort(l, j);
            }
        }
    }
}
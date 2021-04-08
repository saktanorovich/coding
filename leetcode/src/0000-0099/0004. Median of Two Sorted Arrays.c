#define min(a, b) (a) < (b) ? (a) : (b)

double find(int* a, int n, int* b, int m, int k) {
    if (n > m) {
        return find(b, m, a, n, k);
    }
    if (n > 0) {
        if (k == 1) {
            return min(a[0], b[0]);
        } else {
            int x = min(n, k / 2);
            int y = k - x;
            if (a[x - 1] < b[y - 1]) {
                return find(a + x, n - x, b, y, k - x);
            } else {
                return find(a, x, b + y, m - y, k - y);
            }
        }
    }
    return b[k - 1];
}

double findMedianSortedArrays(int* a, int n, int* b, int m){
    int k = n + m;
    if (k % 2) {
        return (find(a, n, b, m, k / 2 + 1));
    } else {
        return (find(a, n, b, m, k / 2 + 1) + find(a, n, b, m, k / 2)) * 0.5;
    }
}

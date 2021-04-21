#include <cstdio>
#include <cmath>
#include <iostream>
using namespace std;

double const PI = 3.1415926535897932384626433832795;
double const ST = 05 * 3600 + 37 * 60;
double const ED = 18 * 3600 + 17 * 60;

int m[200];
int d[200];
int p[200];
int n;
int w;
int h;

int main() {
    for (int testCase = 1; ; ++testCase) {
        scanf("%d", &n);
        if (n == 0) {
            return 0;
        }
        scanf("%d %d", &w, &h);
        for (int i = 1; i < n; ++i) {
            scanf("%d %d", &m[i], &d[i]);
        }
        scanf("%d", &m[n]);
        printf("Apartment Complex: %d\n\n", testCase);
        p[1] = 0;
        for (int i = 2; i <= n; ++i) {
            p[i] = p[i - 1] + d[i - 1] + w;
        }
        for (int q;;) {
            scanf("%d", &q);
            if (q == 0) {
                break;
            }
            int F = q / 100;
            int H = q % 100;
            if (H > n || F > m[H] || H < 1 || F < 1) {
                printf("Apartment %d: Does not exist\n\n", q);
                continue;
            }
            double st = 0;
            double ed = 0;
            for (int i = 1; i <= n; ++i) {
                if (i < H && m[i] >= F) st = max(st, 1.0 * h * (m[i] - F + 1) / (p[H] - p[i] - w));
                if (i > H && m[i] >= F) ed = max(ed, 1.0 * h * (m[i] - F + 1) / (p[i] - p[H] - w));
            }
            int L = (int)(ST + atan(st) * (ED - ST) / PI);
            int R = (int)(ED - atan(ed) * (ED - ST) / PI);
            printf("Apartment %d: %02d:%02d:%02d - %02d:%02d:%02d\n\n", q
                , L / 3600, L % 3600 / 60, L % 60
                , R / 3600, R % 3600 / 60, R % 60);
        }
    }
    return 0;
}
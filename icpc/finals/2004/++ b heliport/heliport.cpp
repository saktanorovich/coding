#include <cstdio>
#include <cmath>
#include <iostream>
using namespace std;

double const eps = 1e-8;

int x[30];
int y[30];
int n, m;

int sign(double x) {
    if (x + eps < 0.0) {
        return -1;
    }
    if (x - eps > 0.0) {
        return +1;
    }
    return 0;
}

int test(double R, double X, double Y) {
    int inside = 0;
    for (int i = 0; i < n; ++i) {
        if (sign(hypot(X - x[i], Y - y[i]) - R) < 0) {
            return 0;
        }
        if (sign(X - x[i]) < 0) {
            int a = sign(Y - y[i]);
            int b = sign(Y - y[i + 1]);
            if (y[i] < y[i + 1]) {
                inside ^= a >= 0 && b < 0;
            }
            if (y[i] > y[i + 1]) {
                inside ^= a < 0 && b >= 0;
            }
        }
    }
    return inside;
}

int okay(double R, double X, double Y) {
   if (!test(R, X, Y)) {
        return 0;
    }
    for (int i = 0; i < n; ++i) {
        if (sign(x[i] - X) != sign(x[i + 1] - X)) {
            if (sign(fabs(y[i] - Y) - R) < 0) {
                return 0;
            }
        }
        if (sign(y[i] - Y) != sign(y[i + 1] - Y)) {
            if (sign(fabs(x[i] - X) - R) < 0) {
                return 0;
            }
        }
    }
    return 1;
}

int okay(double R) {
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < n; ++j) {
            if (okay(R, x[i] + R, y[j] + R)) return 1;
            if (okay(R, x[i] + R, y[j] - R)) return 1;
            if (okay(R, x[i] - R, y[j] + R)) return 1;
            if (okay(R, x[i] - R, y[j] - R)) return 1;
        }
    }
    for (int i = 0; i < n; ++i) {
        for (int j = i + 1; j < n; ++j) {
            double d;
            double h;
            if (x[i] != x[i + 1]) {
                d = fabs(y[j] - (y[i] + R));
                if (sign(d - R) < 0) {
                    h = sqrt(R * R - d * d);
                    if (okay(R, x[j] + h, y[i] + R)) return 1;
                    if (okay(R, x[j] - h, y[i] + R)) return 1;
                }
                d = fabs(y[j] - (y[i] - R));
                if (sign(d - R) < 0) {
                    h = sqrt(R * R - d * d);
                    if (okay(R, x[j] + h, y[i] - R)) return 1;
                    if (okay(R, x[j] - h, y[i] - R)) return 1;
                }
            } else {
                d = fabs(x[j] - (x[i] + R));
                if (sign(d - R) < 0) {
                    h = sqrt(R * R - d * d);
                    if (okay(R, x[i] + R, y[j] + h)) return 1;
                    if (okay(R, x[i] + R, y[j] - h)) return 1;
                }
                d = fabs(x[j] - (x[i] - R));
                if (sign(d - R) < 0) {
                    h = sqrt(R * R - d * d);
                    if (okay(R, x[i] - R, y[j] + h)) return 1;
                    if (okay(R, x[i] - R, y[j] - h)) return 1;
                }
            }
        }
    }
    for (int i = 0; i < n; ++i) {
        for (int j = i + 1; j < n; ++j) {
            double X = 0.5 * (x[i] + x[j]);
            double Y = 0.5 * (y[i] + y[j]);
            double d = hypot(X - x[i], Y - y[i]);
            if (sign(d - R) <= 0) {
                double h = sqrt(R * R - d * d);
                double A = (y[i] - Y) / d;
                double B = (X - x[i]) / d;
                double dx = A * h;
                double dy = B * h;
                if (okay(R, X + dx, Y + dy)) return 1;
                if (okay(R, X - dx, Y - dy)) return 1;
            }
        }
    }
    return 0;
}

int main() {
    char d[10];
    for (int testCase = 1; ; ++testCase) {
        scanf("%d", &n);
        if (n == 0) {
            break;
        }
        x[0] = 0;
        y[0] = 0;
        for (int i = 1; i <= n; ++i) {
            scanf("%d %s", &m, d);
            switch (d[0]) {
                case 'U':
                    x[i] = x[i - 1];
                    y[i] = y[i - 1] + m;
                    break;
                case 'R':
                    x[i] = x[i - 1] + m;
                    y[i] = y[i - 1];
                    break;
                case 'D':
                    x[i] = x[i - 1];
                    y[i] = y[i - 1] - m;
                    break;
                case 'L':
                    x[i] = x[i - 1] - m;
                    y[i] = y[i - 1];
                    break;
                }
            }
            double lo = 0, hi = 1000;
            for (int it = 0; it < 40; ++it) {
                double R = (lo + hi) / 2;
                if (okay(R)) {
                    lo = R;
                } else {
                    hi = R;
                }
            }
            if (testCase > 1) {
                puts("");
            }
            printf("Case Number %d radius is: %.2f\n", testCase, lo);
    }
    return 0;
}
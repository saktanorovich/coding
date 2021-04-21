#include <algorithm>
#include <cstdio>
#include <stack>
#include <vector>
using namespace std;

int const max_number_of_mixtures = 100;
double const eps = 1e-10;

int sign(double x) {
      if (x + eps < 0) {
            return -1;
      }
      if (x - eps > 0) {
            return +1;
      }
      return 0;
}

struct point {
      double x, y, z;
      point() {
      }
      point(double x, double y, double z)
            : x(x), y(y), z(z) {
      }
      bool equals(point const &p) const {
            return (sign(x - p.x) == 0 && sign(y - p.y) == 0 && sign(z - p.z) == 0);
      }
      point operator -(point const &p) const {
            return point(x - p.x, y - p.y, z - p.z);
      }
};

struct mixture {
      int a, b, c;
      mixture() {
      }
      mixture(int a, int b, int c)
            : a(a), b(b), c(c) {
      }
      point normal() const {
            return point(1.0 * a / (a + b + c),
                         1.0 * b / (a + b + c),
                         1.0 * c / (a + b + c));
      }
};

mixture orgnl_mixtures[max_number_of_mixtures];
mixture orgnl_goal;
int number_of_mixtures;

point vector_(point const &a, point const &b) {
      return point(a.y * b.z - a.z * b.y,
                   b.x * a.z - b.z * a.x,
                   a.x * b.y - a.y * b.x);
}

int scalar_(point const &a, point const &b) {
      return sign(a.x * b.x + a.y * b.y + a.z * b.z);
}

int ccw(point const &p, point const &a, point const &b) {
      return scalar_(point(1, 1, 1), vector_(a - p, b - p));
}

double dist(point a, point b) {
      return ((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
}

struct less_angel {
private:
      point _base;
public:
      less_angel(point const &base) {
            _base.x = base.x;
            _base.y = base.y;
            _base.z = base.z;
      }
      bool operator()(point const &p1, point const &p2) const {
            int ccw_ = ccw(_base, p1, p2);
            if (ccw_ >= 0) {
                  if (ccw_ > 0) {
                        return true;
                  }
                  return (sign(dist(_base, p1) - dist(_base, p2)) < 0);
            }
            return false;
      }
};

bool possible(vector<point> &points, point const &goal) {
      int npoints = points.size();
      if (npoints > 1) {
            /* build convex hull of the given set of points on the plane formed by the points (1, 0, 0), (0, 1, 0), (0, 0, 1). */
            point base = points[0];
            for (int i = 1; i < npoints; ++i) {
                  int less = sign(base.x - points[i].x);
                  if (less <= 0) {
                        if (less < 0) {
                              base = points[i];
                        }
                        else {
                              if (sign(base.y - points[i].y) < 0) {
                                    base = points[i];
                              }
                        }
                  }
            }
            sort(points.begin(), points.end(), less_angel(base));
            vector<point> hull;
            if (npoints > 2) {
                  stack<point> s;
                  s.push(points[0]);
                  s.push(points[1]);
                  s.push(points[2]);
                  for (int i = 3; i < npoints; ++i) {
                        while (s.size() > 1) {
                              point curr = s.top(); s.pop();
                              point prev = s.top();
                              point next = points[i];
                              if (ccw(prev, next, curr) >= 0) {
                              }
                              else {
                                    s.push(curr);
                                    break;
                              }
                        }
                        s.push(points[i]);
                  }
                  while (!s.empty()) {
                        hull.push_back(s.top());
                        s.pop();
                  }
                  reverse(hull.begin(), hull.end());
            }
            else {
                  for (int i = 0; i < npoints; ++i) {
                        hull.push_back(points[i]);
                  }
            }
            if (hull.size() > 2) {
                  hull.push_back(hull[0]);
                  for (int i = 0; i + 1 < hull.size(); ++i) {
                        if (ccw(hull[i], hull[i + 1], goal) < 0) {
                              return false;
                        }
                  }
                  return true;
            }
            else {
                  if (ccw(hull[0], hull[1], goal) == 0) {
                        return (scalar_(hull[0] - goal, hull[1] - goal) <= 0);
                  }
                  return false;
            }
      }
      else {
            return points[0].equals(goal);
      }
}

int main() {
      for (int test_case = 1; true; ++test_case) {
            scanf("%d", &number_of_mixtures);
            if (number_of_mixtures == 0) {
                  break;
            }
            for (int i = 0; i < number_of_mixtures; ++i) {
                  scanf("%d %d %d", &orgnl_mixtures[i].a, &orgnl_mixtures[i].b, &orgnl_mixtures[i].c);
            }
            scanf("%d %d %d", &orgnl_goal.a, &orgnl_goal.b, &orgnl_goal.c);
            vector<point> mixtures;
            for (int i = 0; i < number_of_mixtures; ++i) {
                  point normal = orgnl_mixtures[i].normal();
                  int contains = 0;
                  for (int j = 0; j < mixtures.size(); ++j) {
                        if (mixtures[j].equals(normal)) {
                              contains = 1;
                              break;
                        }
                  }
                  if (!contains) {
                        mixtures.push_back(normal);
                  }
            }
            if (test_case > 1) {
                  printf("\n");
            }
            printf("Mixture %d\n", test_case);
            if (possible(mixtures, orgnl_goal.normal())) {
                  printf("Possible\n");
            }
            else {
                  printf("Impossible\n");
            }
      }
      return 0;
}

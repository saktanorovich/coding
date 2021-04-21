#include <algorithm>
#include <cmath>
#include <cstdio>
#include <iostream>
#include <list>
#include <string>
#include <vector>
using namespace std;

int const oo = +1000000000;
double const eps = 1e-9;

inline void __assert(bool condition, string message) {
    if (!condition) {
        throw message;
    }
}

int pprev(int i, int n) {
    return (i - 1 + n) % n;
}

int pnext(int i, int n) {
    return (i + 1 + n) % n;
}

int sign(double x) {
    if (x + eps < 0) {
        return -1;
    }
    if (x - eps > 0) {
        return +1;
    }
    return 0;
}

double det(double a, double b, double c, double d) {
    return a * d - b * c;
}

struct point {
    double x, y;
    point() {
    }
    point(double x, double y) {
        this->x = x;
        this->y = y;
    }
    point operator+(point const &other) const {
        return point(x + other.x, y + other.y);
    }
    point operator-(point const &other) const {
        return point(x - other.x, y - other.y);
    }
    bool operator<(point const &other) const {
        if (sign(x - other.x) != 0) {
            return sign(x - other.x) < 0;
        }
        return sign(y - other.y) < 0;
    }
    bool operator==(point const &other) const {
        if (sign(x - other.x) == 0) {
            return sign(y - other.y) == 0;
        }
        return false;
    }
};

double vector_product(point const &a, point const &b) {
    return a.x * b.y - a.y * b.x;
}

double scalar_product(point const &a, point const &b) {
    return a.x * b.x + a.y * b.y;
}

struct segment {
    point beg;
    point end;
    segment() {
    }
    segment(point const &beg, point const &end) {
        this->beg = beg;
        this->end = end;
    }
    bool contains(point const &point) const {
        return sign(vector_product(beg - point, end - point)) == 0 &&
               sign(scalar_product(beg - point, end - point)) <= 0;
    }
    point* intersect(segment const &other) const;
};

struct line {
    double a, b, c;
    line(point const &p1, point const &p2) {
        a = p1.y - p2.y;
        b = p2.x - p1.x;
        c = -a * p1.x - b * p1.y;
    }
    point* intersect(segment const &segment) const {
        line other(segment.beg, segment.end);
        if (!equivalent(other)) {
            if (!parallel(other)) {
                point* point = intersect(other);
                if (segment.contains(*point)) {
                    return point;
                }
            }
        }
        return 0;
    }
    point* intersect(line const &other) const {
        if (!equivalent(other)) {
            if (!parallel(other)) {
                double x = -1 * det(c, b, other.c, other.b) / det(a, b, other.a, other.b);
                double y = -1 * det(a, c, other.a, other.c) / det(a, b, other.a, other.b);
                return new point(x, y);
            }
        }
        return 0;
    }
    bool contains(point const &point) const {
        return sign(a * point.x + b * point.y + c) == 0;
    }
    bool parallel(line const &other) const {
        return sign(det(a, b, other.a, other.b)) == 0;
    }
    bool equivalent(line const &other) const {
        return sign(det(a, b, other.a, other.b)) == 0 &&
               sign(det(a, c, other.a, other.c)) == 0 &&
               sign(det(b, c, other.b, other.c)) == 0;
    }
    static line make_vertical(double x) {
        return line(point(x, 0), point(x, 1));
    }
};

point* segment::intersect(segment const &other) const {
    line this__line(this->beg, this->end);
    line other_line(other.beg, other.end);
    point* point = this__line.intersect(other_line);
    if (point != 0) {
        if (this->contains(*point) &&
            other.contains(*point)) {
                return point;
            }
    }
    return 0;
}

typedef vector<point> polygon;

struct event {
public:
    point where;
    segment side;
    int polygon;
    event(point* where) {
        this->where = *where;
    }
public:
    bool operator <(event const &other) const {
        return where < other.where;
    }
};

polygon compress(polygon &other) {
    for (bool any = true; any;) {
        any = false;
        int index = 0, nsize = other.size();
        for (polygon::iterator curr = other.begin(); curr != other.end(); ++curr, ++index) {
            segment side(other[pprev(index, nsize)], other[pnext(index, nsize)]);
            if (side.contains(*curr)) {
                any = true;
                other.erase(curr);
                break;
            }
        }
    }
    return other;
}

polygon push(polygon &polygon, point const &point) {
    if (polygon.size() > 0) {
        if (polygon.back() == point || polygon.front() == point) {
            return polygon;
        }
    }
    polygon.push_back(point);
    return polygon;
}

polygon push(polygon &target, polygon const &source, int from, int to) {
    for (int i = from; i <= to; ++i) {
        push(target, source[i]);
    }
    return target;
}

vector<polygon> process(vector<polygon> const &polygons) {
    __assert(polygons.size() == 2, "incorrect number of polygons");
    /* Two cases can be considered: (1) when two polygons are convex and (2) one of them is not
     * a convex. In the 1st case there is only zero or one intersection region while in the 2nd
     * case there are many.. */
    vector<double> xevents;
    for (int seqno = 0; seqno < 2; ++seqno) {
        polygon const &polygon = polygons[seqno];
        for (int i = 0; i < polygon.size(); ++i) {
            xevents.push_back(polygon[i].x);
        }
    }
    int npoints0 = polygons[0].size();
    int npoints1 = polygons[1].size();
    for (int i = 0; i < npoints0; ++i) {
        segment segment0(polygons[0].at(i), polygons[0].at(pnext(i, npoints0)));
        for (int j = 0; j < npoints1; ++j) {
            segment segment1(polygons[1].at(j), polygons[1].at(pnext(j, npoints1)));
            point* where = segment0.intersect(segment1);
            if (where != 0) {
                xevents.push_back(where->x);
            }
        }
    }
    sort(xevents.begin(), xevents.end());
    vector<polygon> regions;
    vector<polygon> result;
    /* move scan-line from the left to the right.. */
    for (int e = 1; e < xevents.size(); ++e) {
        /* analyze scan-beam between le and ri from bottom to top.. */
        line le = line::make_vertical(xevents[e - 1]);
        line ri = line::make_vertical(xevents[e + 0]);
        if (sign(xevents[e] - xevents[e - 1]) > 0) {
            line beam = line::make_vertical(0.5 * (xevents[e] + xevents[e - 1]));
            /* there are no points between x-event points, so find and collect y-events
             * by simple intersection of infinite line and polygons sides.. */
            vector<event> yevents;
            for (int seqno = 0; seqno < 2; ++seqno) {
                polygon const &polygon = polygons[seqno];
                for (int i = 0; i < polygon.size(); ++i) {
                    point point1 = polygon[i];
                    point point2 = polygon[pnext(i, polygon.size())];
                    if (point1.x != point2.x) {
                        segment side(point1, point2);
                        point* where = beam.intersect(side);
                        if (where != 0) {
                            point* le_point = le.intersect(side);
                            point* ri_point = ri.intersect(side);
                            event new_event(where);
                            new_event.side = segment(*le_point, *ri_point);
                            new_event.polygon = seqno;
                            yevents.push_back(new_event);
                        }
                    }
                }
            }
            sort(yevents.begin(), yevents.end());
            vector<int> inside(2, 0);
            for (int i = 0; i < yevents.size(); ++i) {
                if (sign(yevents[i].where.y - yevents[i - 1].where.y) > 0) {
                    if (inside[0] + inside[1] == 2) {
                        /* both polygons have open regions so add the appropriate trapezoid to the list
                         * traversing its vertices in counterclockwise order.. */
                        polygon trapezoid;
                        push(trapezoid, yevents[i - 1].side.beg);
                        push(trapezoid, yevents[i - 1].side.end);
                        push(trapezoid, yevents[i + 0].side.end);
                        push(trapezoid, yevents[i + 0].side.beg);
                        __assert(trapezoid.size() > 2, "degenerate trapezoid");
                        regions.push_back(trapezoid);
                    }
                }
                else {
                    __assert(yevents[i].polygon != yevents[i - 1].polygon, "self-intersection detected");
                }
                inside[yevents[i].polygon] ^= 1;
            }
        }
        else {
            /* nothing to analyze because scan-beam width is zero.. */
        }
    }

    /* merge adjacent regions into a single one.. */
    while (regions.size() > 0) {
        polygon region = regions[0];
        regions.erase(regions.begin());
        for (bool exist_pair_to_merge = true; exist_pair_to_merge;) {
            exist_pair_to_merge = false;
            for (vector<polygon>::iterator curr = regions.begin(); curr != regions.end(); ++curr) {
                polygon trapezoid = *curr;
                for (int p = 0, rsize = region.size(); p < rsize; ++p) {
                    segment region_segment(region[p], region[pnext(p, rsize)]);
                    for (int t = 0, tsize = trapezoid.size(); t < trapezoid.size(); ++t) {
                        segment trapezoid_segment(trapezoid[t], trapezoid[pnext(t, tsize)]);
                        if (region_segment.beg == trapezoid_segment.end &&
                            region_segment.end == trapezoid_segment.beg) {
                                exist_pair_to_merge = true;
                                polygon extended;
                                push(extended, region, 0, p);
                                if (tsize > 3) {
                                    /* in this case two vertices of trapezoid should be pushed back.. */
                                    t = pprev(t, tsize);
                                    t = pprev(t, tsize);
                                    push(extended, trapezoid[t]);
                                    push(extended, trapezoid[pnext(t, tsize)]);
                                }
                                else {
                                    /* in this case one vertex of triangle should be pushed back.. */
                                    push(extended, trapezoid[pprev(t, tsize)]);
                                }
                                region = push(extended, region, p + 1, rsize - 1);
                                regions.erase(curr);
                                goto next_iteration;
                        }
                    }
                }
            }
            next_iteration:;
        }
        result.push_back(compress(region));
    }
    return result;
}

polygon load_polygon() {
    int npoints;
    double x, y;
    scanf("%d", &npoints);
    polygon result;
    for (int i = 0; i < npoints; ++i) {
        scanf("%lf %lf", &x, &y);
        result.push_back(point(x, y));
    }
    return result;
}

int main() {
    /**/
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
    /**/

    for (int test_case = 1; true; ++test_case) {
        vector<polygon> polygons;
        polygons.push_back(load_polygon());
        polygons.push_back(load_polygon());
        if (polygons[0].size() > 0 && polygons[1].size() > 0) {
            vector<polygon> result = process(polygons);
            printf("Data Set %d\n", test_case);
            printf("Number of intersection regions: %d\n", result.size());
            for (int i = 0; i < result.size(); ++i) {
                polygon region = result[i];
                __assert(region.size() > 2, "degenerate region");
                printf("Region %d:", i + 1);
                for (int j = 0; j < region.size(); ++j) {
                    printf("(%.2lf,%.2lf)", region[j].x, region[j].y);
                }
                printf("\n");
            }
        }
        else {
            __assert(polygons[0].size() == 0 && polygons[1].size() == 0, "incorrect input file");
            break;
        }
    }
    return 0;
}

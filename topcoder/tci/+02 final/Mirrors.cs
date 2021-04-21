using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class Mirrors {
            public string seen(string[] mirrors, string[] objects, int[] start) {
                  List<string> obj = new List<string>(objects);
                  obj.Add(string.Format("me {0} {1}", start[0], start[1]));
                  return seen(union(Mirror.parse(mirrors), Obstacle.parse(obj.ToArray())),
                                          new Ray(new Point(start[0], start[1]), start[2] * MathUtils.pi / 180));
            }

            private string seen(List<Model2D> models, Ray ray) {
                  while (true) {
                        Model2D where = ray.intersect(models);
                        if (where != null) {
                              if (where is Obstacle) {
                                    return where.name;
                              }
                              ray = where.reflect(ray);
                        }
                        else break;
                  }
                  return "space";
            }

            private List<Model2D> union(List<Mirror> mirrors, List<Obstacle> obstacles) {
                  List<Model2D> result = new List<Model2D>();
                  foreach (Model2D model in mirrors) {
                        result.Add(model);
                  }
                  foreach (Model2D model in obstacles) {
                        result.Add(model);
                  }
                  return result;
            }

            private class Point {
                  public double x;
                  public double y;

                  public Point(double x, double y) {
                        this.x = x;
                        this.y = y;
                  }

                  public double distance(Point other) {
                        return Math.Sqrt((other.x - this.x) * (other.x - this.x) + (other.y - this.y) * (other.y - this.y));
                  }

                  public static Point operator +(Point a, Point b) {
                        return new Point(a.x + b.x, a.y + b.y);
                  }

                  public static Point operator -(Point a, Point b) {
                        return new Point(a.x - b.x, a.y - b.y);
                  }

                  public static Point operator *(Point p, double scalar) {
                        return new Point(p.x * scalar, p.y * scalar);
                  }
            }

            private class Line : Model2D {
                  public double A, B, C;

                  public Line(Point a, Point b) {
                        A = a.y - b.y;
                        B = b.x - a.x;
                        C = a.x * b.y - a.y * b.x;
                  }

                  public override Ray reflect(Ray ray) {
                        Point point = intersect(ray);
                        if (point != null) {
                              return new Ray(point, (ray.end - ray.beg) - normal() * (2 * MathUtils.scalar(ray.end - ray.beg, normal())));
                        }
                        return ray;
                  }

                  public override Point intersect(Ray ray) {
                        Line line = new Line(ray.beg, ray.end);
                        if (!this.parallel(line)) {
                              double x = -1 * MathUtils.det(this.C, this.B, line.C, line.B) / MathUtils.det(this.A, this.B, line.A, line.B);
                              double y = -1 * MathUtils.det(this.A, this.C, line.A, line.C) / MathUtils.det(this.A, this.B, line.A, line.B);
                              if (ray.contains(new Point(x, y))) {
                                    return new Point(x,y);
                              }
                        }
                        return null;
                  }

                  public override bool contains(Point point) {
                        return MathUtils.sign(A * point.x + B * point.y + C) == 0;
                  }

                  public double distance() {
                        return MathUtils.abs(C) / Math.Sqrt(A * A + B * B);
                  }

                  public Point normal() {
                        double x = +A / Math.Sqrt(A * A + B * B);
                        double y = +B / Math.Sqrt(A * A + B * B);
                        return new Point(x, y);
                  }

                  public Point direct() {
                        double x = -B / Math.Sqrt(A * A + B * B);
                        double y = +A / Math.Sqrt(A * A + B * B);
                        return new Point(x, y);
                  }

                  public bool parallel(Line line) {
                        return MathUtils.sign(MathUtils.det(this.A, this.B, line.A, line.B)) == 0;
                  }
            }

            private class Mirror : Model2D {
                  public Point beg;
                  public Point end;

                  public override Ray reflect(Ray ray) {
                        Point point = intersect(ray);
                        if (point != null) {
                              return new Line(beg, end).reflect(ray);
                        }
                        return ray;
                  }

                  public override Point intersect(Ray ray) {
                        Point point = new Line(beg, end).intersect(ray);
                        if (point != null) {
                              if (this.contains(point)) {
                                    return point;
                              }
                        }
                        return null;
                  }

                  public override bool contains(Point point) {
                        return MathUtils.sign(MathUtils.vector(beg - point, end - point)) == 0 &&
                               MathUtils.sign(MathUtils.scalar(beg - point, end - point)) <= 0;
                  }

                  public static List<Mirror> parse(string[] mirrors) {
                        List<Mirror> result = new List<Mirror>();
                        for (int i = 0; i < mirrors.Length; ++i) {
                              int[] coord = Array.ConvertAll(mirrors[i].Split(new char[] { ' ' }),
                                    delegate(string s) {
                                          return int.Parse(s);
                              });
                              Mirror res = new Mirror();
                              res.beg = new Point(coord[0], coord[1]);
                              res.end = new Point(coord[2], coord[3]);
                              result.Add(res);
                        }
                        return result;
                  }
            }

            private class Obstacle : Model2D {
                  public Point center;

                  public override Ray reflect(Ray ray) {
                        return ray;
                  }

                  public override Point intersect(Ray ray) {
                        Line line = new Line(ray.beg - center, ray.end - center);
                        double h = line.distance();
                        if (MathUtils.sign(h - 1) <= 0) {
                              Point nearest = center + line.normal() * -MathUtils.sign(line.C) * h;
                              if (MathUtils.sign(h - 1) == 0) {
                                    if (ray.contains(nearest)) {
                                          return nearest;
                                    }
                              }
                              else {
                                    Point p1 = nearest + line.direct() * Math.Sqrt(1 - h * h);
                                    Point p2 = nearest - line.direct() * Math.Sqrt(1 - h * h);
                                    if (ray.contains(p1) && ray.contains(p2)) {
                                          return MathUtils.sign(ray.beg.distance(p1) - ray.beg.distance(p2)) < 0 ? p1 : p2;
                                    }
                                    if (ray.contains(p1)) return p1;
                                    if (ray.contains(p2)) return p2;
                              }
                        }
                        return null;
                  }

                  public override bool contains(Point point) {
                        return MathUtils.sign(center.distance(point) - 1) <= 0;
                  }

                  public static List<Obstacle> parse(string[] objects) {
                        List<Obstacle> result = new List<Obstacle>();
                        foreach (string obj in objects) {
                              string[] items = obj.Split(new char[] { ' ' });
                              Obstacle res = new Obstacle();
                              res.name = items[0];
                              res.center = new Point(int.Parse(items[1]), int.Parse(items[2]));
                              result.Add(res);
                        }
                        return result;
                  }
            }

            private class Ray {
                  public Point beg;
                  public Point end;

                  public Ray(Point beg, double look) {
                        this.beg = beg;
                        this.end = new Point(Math.Cos(look) + beg.x, Math.Sin(look) + beg.y);
                  }

                  public Ray(Point beg, Point vector) {
                        this.beg = beg;
                        this.end = beg + vector;
                  }

                  public bool contains(Point point) {
                        return MathUtils.sign(MathUtils.vector(point - beg, end - beg)) == 0 &&
                               MathUtils.sign(MathUtils.scalar(point - beg, end - beg)) >= 0;
                  }

                  public Model2D intersect(List<Model2D> models) {
                        Model2D result = null; Point nearest = null;
                        foreach (Model2D model in models) {
                              if (!model.contains(beg)) {
                                    Point point = model.intersect(this);
                                    if (point != null) {
                                          if (nearest == null || MathUtils.sign(beg.distance(point) - beg.distance(nearest)) < 0) {
                                                nearest = point;
                                                result = model;
                                          }
                                    }
                              }
                        }
                        return result;
                  }
            }

            private abstract class Model2D {
                  public string name;

                  public abstract Ray reflect(Ray ray);
                  public abstract Point intersect(Ray ray);
                  public abstract bool contains(Point point);
            }

            private static class MathUtils {
                  public static readonly double eps = 1e-9;
                  public static readonly double pi = 3.1415926535897932384626433832795;

                  public static int sign(double x) {
                        if (x + eps < 0) {
                              return -1;
                        }
                        if (x - eps > 0) {
                              return +1;
                        }
                        return 0;
                  }

                  public static double abs(double x) {
                        if (sign(x) < 0) {
                              return -x;
                        }
                        return x;
                  }

                  public static double vector(Point a, Point b) {
                        return a.x * b.y - a.y * b.x;
                  }

                  public static double scalar(Point a, Point b) {
                        return a.x * b.x + a.y * b.y;
                  }

                  public static double det(double a, double b, double c, double d) {
                        return a * d - b * c;
                  }
            }
      }
}
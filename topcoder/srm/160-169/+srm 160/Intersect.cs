using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopCoder.Algorithm {
      public class Intersect {
            public int area(int[] x, int[] y) {
                  List<Rectangle> rectangles = new List<Rectangle>();
                  for (int i = 0; i < x.Length; i += 2) {
                        rectangles.Add(new Rectangle(x[i], y[i], x[i + 1], y[i + 1]));
                  }
                  return area(rectangles);
            }

            private int area(List<Rectangle> rectangles) {
                  int xmin = int.MinValue;
                  int ymin = int.MinValue;
                  int xmax = int.MaxValue;
                  int ymax = int.MaxValue;
                  foreach (Rectangle rectangle in rectangles) {
                        xmin = Math.Max(xmin, rectangle.xmin);
                        ymin = Math.Max(ymin, rectangle.ymin);
                        xmax = Math.Min(xmax, rectangle.xmax);
                        ymax = Math.Min(ymax, rectangle.ymax);
                  }
                  if (xmin < xmax && ymin < ymax) {
                        return (xmax - xmin) * (ymax - ymin);
                  }
                  return 0;
            }

            private class Rectangle {
                  public int xmin;
                  public int ymin;
                  public int xmax;
                  public int ymax;

                  public Rectangle(int x0, int y0, int x1, int y1) {
                        this.xmin = Math.Min(x0, x1);
                        this.ymin = Math.Min(y0, y1);
                        this.xmax = Math.Max(x0, x1);
                        this.ymax = Math.Max(y0, y1);
                  }
            }
      }
}
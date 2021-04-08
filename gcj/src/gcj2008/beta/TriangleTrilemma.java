package gcj2008.beta;

import utils.io.*;
import java.io.*;

// Problem A
public class TriangleTrilemma {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int x1 = in.nextInt();
        int y1 = in.nextInt();
        int x2 = in.nextInt();
        int y2 = in.nextInt();
        int x3 = in.nextInt();
        int y3 = in.nextInt();
        out.format("Case #%d: %s\n", testCase, get(x1, y1, x2, y2, x3, y3));
    }

    private String get(int x1, int y1, int x2, int y2, int x3, int y3) {
        if (vector(x2 - x1, y2 - y1, x3 - x1, y3 - y1) != 0) {
            StringBuilder res = new StringBuilder();
            res.append(lengthsOfSides(x1, y1, x2, y2, x3, y3));
            res.append(internalAngles(x1, y1, x2, y2, x3, y3));
            res.append("triangle");
            return res.toString();
        }
        return "not a triangle";
    }

    private String lengthsOfSides(int x1, int y1, int x2, int y2, int x3, int y3) {
        int a = length(x2 - x1, y2 - y1);
        int b = length(x3 - x1, y3 - y1);
        int c = length(x3 - x2, y3 - y2);
        if (a != b && a != c && b != c) {
            return "scalene ";
        } else {
            return "isosceles ";
        }
    }

    private String internalAngles(int x1, int y1, int x2, int y2, int x3, int y3) {
        int a = scalar(x2 - x1, y2 - y1, x3 - x1, y3 - y1);
        int b = scalar(x1 - x2, y1 - y2, x3 - x2, y3 - y2);
        int c = scalar(x2 - x3, y2 - y3, x1 - x3, y1 - y3);
        if (a == 0 || b == 0 || c == 0) {
            return "right ";
        }
        if (a < 0 || b < 0 || c < 0) {
            return "obtuse ";
        }
        return "acute ";
    }

    private static int vector(int ax, int ay, int bx, int by) {
        return ax * by - ay * bx;
    }

    private static int scalar(int ax, int ay, int bx, int by) {
        return ax * bx + ay * by;
    }

    private static int length(int ax, int ay) {
        return ax * ax + ay * ay;
    }
}

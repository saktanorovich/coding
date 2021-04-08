package practice.contest1;

import java.io.*;
import utils.io.*;

// Problem A
public class OldMagician {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int w = in.nextInt();
        int b = in.nextInt();
        out.format("Case #%d: %s\n", testCase, b % 2 == 0 ? "WHITE" : "BLACK");
    }
}

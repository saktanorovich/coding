import java.io.*;
import utils.io.*;

import gcj2008.beta.*;
import gcj2008.qual.*;
import gcj2008.rd1a.*;
import gcj2008.rd1b.*;
import gcj2008.rd1c.*;
import gcj2008.rd2.*;
import gcj2008.rd3.*;
import gcj2008.semiAMER.*;
import gcj2008.semiAPAC.*;
import gcj2008.semiEMEA.*;
import gcj2008.wfinals.*;

import gcj2017.qual.*;

import kickstart2017.practice.*;
import kickstart2017.rounda.*;
import kickstart2017.roundb.*;
import kickstart2017.roundc.*;
import kickstart2017.roundd.*;
import kickstart2017.rounde.*;
import kickstart2017.roundf.*;
import kickstart2017.roundg.*;

import kickstart2018.practice.*;
import kickstart2018.rounda.*;

import practice.contest1.*;
import practice.contest2.*;

import women.io2016.*;
import women.io2017.*;

public class Main {
    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new utils.io.InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        int testCases = in.nextInt();
        System.out.format("Test Case: Elapsed time\n", testCases);
        for (int test = 1; test <= testCases; ++test) {
            long beg = System.nanoTime();
            //new King().process(test, in, out);
            new TheYearOfCodeJam().process(test, in, out);
            out.flush();
            long end = System.nanoTime();
            System.out.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
        }

        in.close();
        out.close();
    }
}

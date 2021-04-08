package practice.contest2;

import java.io.*;
import java.math.*;
import utils.io.*;

// Problem A
public class AlienNumbers {
    public void process(int testCase, InputReader in, PrintWriter out) {
        String number = in.next();
        String source = in.next();
        String target = in.next();
        out.format("Case #%d: %s\n", testCase, toSystem(toDecimal(number, source), target));
    }

    private static int toDecimal(String number, String digits) {
        int sys = digits.length();
        int pow = 1;
        int res = 0;
        for (int i = number.length() - 1; i >= 0; --i) {
            res += pow * digits.indexOf(number.charAt(i));
            pow *= sys;
        }
        return res;
    }

    private static String toSystem(int decimal, String digits) {
        int sys = digits.length();
        StringBuilder res = new StringBuilder();
        while (decimal >= sys) {
            res.append(digits.charAt(decimal % sys));
            decimal /= sys;
        }
        if (decimal > 0) {
            res.append(digits.charAt(decimal));
        }
        return res.reverse().toString();
    }
    /*
    private static BigInteger toDecimal(String number, String digits) {
        BigInteger sys = BigInteger.valueOf(digits.length());
        BigInteger pow = BigInteger.ONE;
        BigInteger res = BigInteger.ZERO;
        for (int i = number.length() - 1; i >= 0; --i) {
            BigInteger digit = BigInteger.valueOf(digits.indexOf(number.charAt(i)));
            res = res.add(pow.multiply(digit));
            pow = pow.multiply(sys);
        }
        return res;
    }

    private static String toSystem(BigInteger decimal, String digits) {
        BigInteger sys = BigInteger.valueOf(digits.length());
        StringBuilder res = new StringBuilder();
        while (decimal.compareTo(sys) >= 0) {
            res.append(digits.charAt(decimal.mod(sys).intValue()));
            decimal = decimal.divide(sys);
        }
        if (decimal.equals(0) == false) {
            res.append(digits.charAt(decimal.mod(sys).intValue()));
        }
        return res.reverse().toString();
    }
    */
}

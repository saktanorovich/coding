package women.io2016;

import utils.io.*;
import java.io.*;
import java.util.*;

// Problem D
public class PasswordSecurity {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int passwordsCount = in.nextInt();
        String[] passwords = new String[passwordsCount];
        for (int i = 0; i < passwordsCount; ++i) {
            passwords[i] = in.next();
        }
        out.format("Case #%d: %s\n", testCase, toString(get(passwords)));
    }

    private List<Character> get(String[] passwords) {
        for (String pass : passwords) {
            if (pass.length() == 1) {
                return null;
            }
        }
        List<Character> guess = new ArrayList<>();
        for (int i = 0; i < 26; ++i) {
            guess.add((char)('A' + i));
        }
        Random rand = new Random();
        for (int i = 0; i < (int)1e5; ++i) {
            Collections.shuffle(guess, rand);
            if (hamilton(guess, passwords)) {
                return guess;
            }
        }
        return null;
    }

    private boolean hamilton(List<Character> guess, String[] passwords) {
        String s = toString(guess);
        for (String pass : passwords) {
            if (s.indexOf(pass) >= 0) {
                return false;
            }
        }
        return true;
    }

    private static String toString(List<Character> s) {
        if (s != null) {
            StringBuilder res = new StringBuilder();
            for (Character c : s) {
                res.append(c);
            }
            return res.toString();
        }
        return "IMPOSSIBLE";
    }
}

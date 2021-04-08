package kickstart2017.practice;

import java.io.*;
import java.util.*;
import utils.io.*;

// Problem A
public class CountryLeader {
    public void process(int testCase, InputReader in, PrintWriter out) {
        int n = in.nextInt();
        String[] names = new String[n];
        for (int i = 0; i < n; ++i) {
            names[i] = in.nextLine();
        }
        Arrays.sort(names, (a, b) -> {
            int sa = score(a);
            int sb = score(b);
            if (sb != sa) {
                return -(sa - sb);
            }
            return a.compareTo(b);
        });
        out.format("Case #%d: %s\n", testCase, names[0]);
    }

    private int score(String s) {
        HashSet<Character> chars = new HashSet<>();
        for (Character c : s.toCharArray()) {
            if ('A' <= c && c <= 'Z') {
                chars.add(c);
            }
        }
        return chars.size();
    }
}

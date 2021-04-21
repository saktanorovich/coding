import java.util.*;

    public class MatchMaking {
        public String makeMatch(String[] w, String[] aw, String[] n, String[] an, String qw) {
            Person[] wp = new Person[w.length];
            for (int i = 0; i < w.length; ++i) {
                wp[i] = new Person(w[i], aw[i]);
            }
            Person[] mp = new Person[n.length];
            for (int i = 0; i < n.length; ++i) {
                mp[i] = new Person(n[i], an[i]);
            }
            return makeMatch(wp, mp, qw, wp.length);
        }

        public String makeMatch(Person[] w, Person[] m, String qw, int n) {
            Arrays.sort(w);
            Arrays.sort(m);
            String[] w2m = new String[n];
            String[] m2w = new String[n];
            for (int i = 0; i < n; ++i) {
                int k = -1;
                int c = -1;
                for (int j = 0; j < n; ++j) {
                    if (m2w[j] == null) {
                        int t = w[i].match(m[j]);
                        if (c < t) {
                            c = t;
                            k = j;
                        }
                    }
                }
                w2m[i] = m[k].name;
                m2w[k] = w[i].name;
                if (w[i].name.equals(qw)) {
                    return m[k].name;
                }
            }
            return null;
        }

        private final class Person implements Comparable<Person> {
            public final String name;
            public final String answ;

            public Person(String name, String answ) {
                this.name = name;
                this.answ = answ;
            }

            public int match(Person other) {
                int res = 0;
                for (int i = 0; i < answ.length(); ++i) {
                    if (answ.charAt(i) == other.answ.charAt(i)) {
                        ++res;
                    }
                }
                return res;
            }

            public int compareTo(Person other) {
                return name.compareTo(other.name);
            }
        }
    }

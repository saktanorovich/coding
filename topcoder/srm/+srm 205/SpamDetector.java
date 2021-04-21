import java.util.*;

    public class SpamDetector {
        public int countKeywords(String subjectLine, String[] keywords) {
            return countKeywords(subjectLine.split(" "), buildPatterns(keywords));
        }

        private static int countKeywords(String[] subject, String[] keywords) {
            int res = 0;
            for (String line : subject) {
                String word = line.toLowerCase();
                for (String keyword : keywords) {
                    if (word.matches(keyword)) {
                        ++res;
                        break;
                    }
                }
            }
            return res;
        }

        private static String[] buildPatterns(String[] keywords) {
            String[] res = new String[keywords.length];
            for (int i = 0; i < keywords.length; ++i) {
                String keyword = keywords[i].toLowerCase();
                StringBuilder builder = new StringBuilder();
                for (int j = 0; j < keyword.length(); ++j) {
                    builder.append(keyword.charAt(j) + "+");
                }
                res[i] = builder.toString();
            }
            return res;
        }
    }

import java.util.*;

    public class Hyphenated {
        public double avgLength(String[] lines) {
            return avgLength(String.join("\n", lines));
        }

        private double avgLength(String text) {
            text = text.replaceAll("-\n([a-zA-z])", "?");
            text = text.replaceAll("[-\n.]", " ");
            String[] words = text.split(" ");
            double total = 0;
            double count = 0;
            for (String word : words) {
                if (word.length() > 0) {
                    total += word.length();
                    count += 1;
                }
            }
            return total / count;
        }
    }
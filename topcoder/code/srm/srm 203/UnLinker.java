import java.util.*;
import java.util.regex.*;

    public class UnLinker {
        public String clean(String text) {
            Pattern pattern = Pattern.compile(WEBLINK);
            for (int i = 1; true; ++i) {
                Matcher matcher = pattern.matcher(text);
                if (matcher.find()) {
                    text = matcher.replaceFirst(String.format("OMIT%d", i));
                }
                else break;
            }
            return text;
        }

        private static final String WEBLINK = "(http://|http://www\\.|www\\.)([a-zA-z0-9\\.]+)(\\.com|\\.org|\\.edu|\\.info|\\.tv)";
    }

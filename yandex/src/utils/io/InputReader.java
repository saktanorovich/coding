package utils.io;

import java.io.*;
import java.util.*;

public class InputReader {
    private BufferedReader reader;
    private StringTokenizer tokenizer;

    public InputReader(InputStream input) {
        reader = new BufferedReader(new InputStreamReader(input), 32768);
        tokenizer = null;
    }

    public boolean hasNext() {
        while (tokenizer == null || tokenizer.hasMoreTokens() == false) {
            try {
                String nextLine = reader.readLine();
                if (nextLine != null) {
                    tokenizer = new StringTokenizer(nextLine);
                } else {
                    return false;
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        return tokenizer.hasMoreTokens();
    }

    public String next() {
        if (hasNext()) {
            return tokenizer.nextToken();
        }
        return null;
    }

    public String nextLine() {
        try {
            return reader.readLine();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    public int nextInt() {
        return Integer.parseInt(next());
    }

    public long nextLong() {
        return Long.parseLong(next());
    }

    public double nextDouble() {
        return Double.parseDouble(next());
    }

    public void close() throws IOException {
        reader.close();
        reader = null;
    }
}
import java.io.*;
import java.util.*;

public class Main {
    private static final class CodeGeneration {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            if (testCase > 1) {
                out.format("\n");
            }
            SyntaxTree syntax = SyntaxTree.parse(in.nextLine());
            new Assembly(out).emit(syntax);
            return true;
        }

        private static final class Assembly {
            private final PrintWriter writer;
            private final boolean memory[];

            public Assembly(PrintWriter writer) {
                this.writer = writer;
                this.memory = new boolean[256];
            }

            public void emit(SyntaxTree syntax) {
                emit(syntax.root);
            }

            private void emit(SyntaxNode node) {
                if (Character.isAlphabetic(node.element)) {
                    writer.format("L %c\n", node.element);
                    return;
                }
                if (node.element == '@') {
                    emit(node.children.get(0));
                    writer.format("N\n");
                    return;
                }
                SyntaxNode l = node.children.get(0);
                SyntaxNode r = node.children.get(1);
                emit(l);
                if (node.element == '+') {
                    if (r.numOfCommands > 0) {
                        int memIdx = alloc();
                        writer.format("ST $%d\n", memIdx);
                        emit(r);
                        writer.format("A $%d\n", memIdx);
                        free(memIdx);
                    } else {
                        writer.format("A %c\n", r.element);
                    }
                } else if (node.element == '*') {
                    if (r.numOfCommands > 0) {
                        int memIdx = alloc();
                        writer.format("ST $%d\n", memIdx);
                        emit(r);
                        writer.format("M $%d\n", memIdx);
                        free(memIdx);
                    } else {
                        writer.format("M %c\n", r.element);
                    }
                } else if (node.element == '-') {
                    if (r.numOfCommands > 0) {
                        int memIdx = alloc();
                        writer.format("ST $%d\n", memIdx);
                        emit(r);
                        writer.format("N\n");
                        writer.format("A $%d\n", memIdx);
                        free(memIdx);
                    } else {
                        writer.format("S %c\n", r.element);
                    }
                } else if (node.element == '/') {
                    if (r.numOfCommands > 0) {
                        int memIdxL = alloc();
                        writer.format("ST $%d\n", memIdxL);
                        emit(r);
                        int memIdxR = alloc();
                        writer.format("ST $%d\n", memIdxR);
                        writer.format("L $%d\n", memIdxL);
                        writer.format("D $%d\n", memIdxR);
                        free(memIdxR);
                        free(memIdxL);
                    } else {
                        writer.format("D %c\n", r.element);
                    }
                }
            }

            private int alloc() {
                for (int i = 0; i < memory.length; ++i) {
                    if (memory[i] == false) {
                        memory[i] = true;
                        return i;
                    }
                }
                throw new RuntimeException();
            }

            private void free(int memIdx) {
                memory[memIdx] = false;
            }
        }

        private static final class SyntaxNode {
            public final List<SyntaxNode> children;
            public final char element;
            public int numOfCommands;
            public int numOfOperands;

            public SyntaxNode(char element) {
                this.children = new ArrayList<>();
                this.element = element;
                if (Character.isAlphabetic(element)) {
                    numOfOperands = 1;
                } else {
                    numOfCommands = 1;
                }
            }

            public void add(SyntaxNode child) {
                children.add(child);
                numOfCommands += child.numOfCommands;
                numOfOperands += child.numOfOperands;
            }
        }

        private static final class SyntaxTree {
            public final SyntaxNode root;

            private SyntaxTree(SyntaxNode root) {
                this.root = root;
            }

            public static SyntaxTree parse(String expression) {
                Stack<SyntaxNode> stack = new Stack<>();
                for (char c : expression.toCharArray()) {
                    SyntaxNode t = new SyntaxNode(c);
                    if (Character.isAlphabetic(c)) {
                        stack.push(t);
                    }
                    else if (c == '@') {
                        t.add(stack.pop());
                        stack.push(t);
                    }
                    else {
                        SyntaxNode r = stack.pop();
                        SyntaxNode l = stack.pop();
                        t.add(l);
                        t.add(r);
                        stack.push(t);
                    }
                }
                return new SyntaxTree(stack.pop());
            }
        }
    }

    public static void main(String[] args) throws Exception {
        InputReader in = new InputReader(new FileInputStream("input.txt"));
        PrintWriter out = new PrintWriter(new FileOutputStream("output.txt"));
        //InputReader in = new InputReader(System.in);
        //PrintWriter out = new PrintWriter(System.out);

        if (args.length > 0 && args[0].equals("-g")) {
        } else {
            System.err.println("Test Case: Elapsed time");
            boolean contd = true;
            for (int test = 1; in.hasNext(); ++test) {
                long beg = System.nanoTime();
                contd = new CodeGeneration().process(test, in, out);
                out.flush();
                long end = System.nanoTime();
                System.err.format("Test #%3d: %9.3f ms\n", test, (end - beg) / 1e6);
            }
        }

        in.close();
        out.close();
    }

    private static final class InputReader {
        private BufferedReader reader;
        private String nextLine;

        public InputReader(InputStream input) {
            reader = new BufferedReader(new InputStreamReader(input), 32768);
        }

        public boolean hasNext() {
            try {
                nextLine = reader.readLine();
                if (nextLine != null) {
                    return true;
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
            return false;
        }

        public String nextLine() {
            if (nextLine == null) {
                hasNext();
            }
            String result = nextLine;
            nextLine = null;
            return result;
        }

        public void close() throws IOException {
            reader.close();
            reader = null;
        }
    }
}

import java.io.*;
import java.util.*;

public class Main {
    private static final class CodeGeneration {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            SyntaxTree syntax = SyntaxTree.parse(in.nextLine());
            Assembly asm = new Assembly();
            List<Instruction> instr = asm.emit(syntax);
            if (testCase > 1) {
                out.println();
            }
            for (Instruction i : instr) {
                out.println(i.toString());
            }
            return true;
        }

        private static final class Instruction {
            public final String code;
            public final String args;

            public Instruction(String code, String args) {
                this.code = code;
                this.args = args;
            }

            @Override
            public String toString() {
                if (args == null) {
                    return String.format("%s", code);
                } else {
                    return String.format("%s %s", code, args);
                }
            }
        }

        private static final class Assembly {
            private boolean memused[];

            public Assembly() {
                this.memused = new boolean[256];
            }

            public List<Instruction> emit(SyntaxTree syntax) {
                return emit(syntax.root, new ArrayList<>());
            }

            private List<Instruction> emit(SyntaxNode node, List<Instruction> instr) {
                if (Character.isAlphabetic(node.element)) {
                    instr.add(new Instruction("L", String.format("%c", node.element)));
                }
                else if (node.element == '@') {
                    emit(node.children.get(0), instr);
                    instr.add(new Instruction("N", null));
                }
                else {
                    SyntaxNode l = node.children.get(0);
                    SyntaxNode r = node.children.get(1);
                    emit(l, instr);
                    if (node.element == '+') {
                        if (r.numOfCommands > 0) {
                            int memIdx = alloc();
                            instr.add(new Instruction("ST", String.format("$%d", memIdx)));
                            emit(r, instr);
                            instr.add(new Instruction("A",  String.format("$%d", memIdx)));
                            free(memIdx);
                        } else {
                            assert Character.isAlphabetic(r.element);
                            instr.add(new Instruction("A",  String.format("%c", r.element)));
                        }
                    } else if (node.element == '*') {
                        if (r.numOfCommands > 0) {
                            int memIdx = alloc();
                            instr.add(new Instruction("ST", String.format("$%d", memIdx)));
                            emit(r, instr);
                            instr.add(new Instruction("M",  String.format("$%d", memIdx)));
                            free(memIdx);
                        } else {
                            assert Character.isAlphabetic(r.element);
                            instr.add(new Instruction("M",  String.format("%c", r.element)));
                        }
                    } else if (node.element == '-') {
                        if (r.numOfCommands > 0) {
                            int memIdx = alloc();
                            instr.add(new Instruction("ST", String.format("$%d", memIdx)));
                            emit(r, instr);
                            instr.add(new Instruction("N",  null));
                            instr.add(new Instruction("A",  String.format("$%d", memIdx)));
                            free(memIdx);
                        } else {
                            assert Character.isAlphabetic(r.element);
                            instr.add(new Instruction("S",  String.format("%c", r.element)));
                        }
                    } else if (node.element == '/') {
                        if (r.numOfCommands > 0) {
                            int memIdxL = alloc();
                            instr.add(new Instruction("ST", String.format("$%d", memIdxL)));
                            emit(r, instr);
                            int memIdxR = alloc();
                            instr.add(new Instruction("ST", String.format("$%d", memIdxR)));
                            instr.add(new Instruction("L",  String.format("$%d", memIdxL)));
                            instr.add(new Instruction("D",  String.format("$%d", memIdxR)));
                            free(memIdxR);
                            free(memIdxL);
                        } else {
                            assert Character.isAlphabetic(r.element);
                            instr.add(new Instruction("D",  String.format("%c", r.element)));
                        }
                    }
                }
                return instr;
            }

            private int alloc() {
                for (int i = 0; i < memused.length; ++i) {
                    if (memused[i] == false) {
                        memused[i] = true;
                        return i;
                    }
                }
                throw new RuntimeException();
            }

            private void free(int memIdx) {
                memused[memIdx] = false;
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
                    numOfOperands = numOfOperands + 1;
                } else {
                    numOfCommands = numOfCommands + 1;
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
                    if (Character.isAlphabetic(c)) {
                        stack.push(new SyntaxNode(c));
                        continue;
                    }
                    SyntaxNode t = new SyntaxNode(c);
                    switch (c) {
                        case '+':
                        case '*':
                        case '-':
                        case '/':
                            SyntaxNode r = stack.pop();
                            SyntaxNode l = stack.pop();
                            t.add(l);
                            t.add(r);
                            stack.push(t);
                            break;
                        case '@': {
                            t.add(stack.pop());
                            stack.push(t);
                            break;
                        }
                        default:
                            throw new RuntimeException();
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

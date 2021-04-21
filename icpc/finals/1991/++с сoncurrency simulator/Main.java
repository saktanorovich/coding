import java.io.*;
import java.util.*;

public class Main {
    private static final class ConcurrencySimulator {
        public boolean process(int testCase, InputReader in, PrintWriter out) {
            if (testCase > 1) {
                out.format("\n");
            }
            Program programs[] = new Program[in.nextInt()];
            Processor processor = new Processor(in, out);
            for (int i = 0; i < programs.length; ++i) {
                programs[i] = Program.read(i + 1, in);
            }
            processor.exec(programs);
            return true;
        }

        private static final class Processor {
            private final PrintWriter writer;
            private final int variables[];
            private final int execTime[];
            private final int quantum;
            private final LinkedList<Program> readyQueue;
            private final LinkedList<Program> blockQueue;
            private boolean lockHeld;

            public Processor(InputReader in, PrintWriter out) {
                this.variables = new int[26];
                this.execTime = new int[5];
                for (int i = 0; i < 5; ++i) {
                    this.execTime[i] = in.nextInt();
                }
                this.quantum = in.nextInt();
                this.writer = out;
                this.readyQueue = new LinkedList<>();
                this.blockQueue = new LinkedList<>();
            }

            public void exec(Program[] programs) {
                for (Program p : programs) {
                    readyQueue.add(p);
                }
                while (readyQueue.isEmpty() == false) {
                    exec(readyQueue.poll());
                }
            }

            private void exec(Program prog) {
                for (int time = quantum; time > 0; prog.next()) {
                    Instruction instr = prog.peek();
                    if (instr.type == 4) {
                        return;
                    }
                    else if (instr.type == 3) {
                        lockHeld = false;
                        if (blockQueue.isEmpty() == false) {
                            readyQueue.addFirst(blockQueue.poll());
                        }
                    }
                    else if (instr.type == 2) {
                        if (lockHeld) {
                            blockQueue.add(prog);
                            return;
                        }
                        lockHeld = true;
                    }
                    else if (instr.type == 1) {
                        writer.format("%d: %d\n", prog.pid, variables[instr.args[0].charAt(0) - 'a']);
                    }
                    else if (instr.type == 0) {
                        variables[instr.args[0].charAt(0) - 'a'] = Integer.parseInt(instr.args[1]);
                    }
                    time -= execTime[instr.type];
                }
                readyQueue.add(prog);
            }
        }

        private static final class Program {
            public final List<Instruction> instr;
            public final int pid;
            private int ptr;

            public Program(int pid, List<Instruction> instr) {
                this.instr = instr;
                this.pid = pid;
                this.ptr = 0;
            }

            public Instruction peek() {
                if (ptr < instr.size()) {
                    return instr.get(ptr);
                }
                return null;
            }

            public void next() {
                ptr = ptr + 1;
            }

            public static Program read(int pid, InputReader in) {
                List<Instruction> instr = new ArrayList<>();
                while (true) {
                    Instruction i = Instruction.read(in);
                    instr.add(i);
                    if (i.type == 4) {
                        break;
                    }
                }
                return new Program(pid, instr);
            }
        }

        private static final class Instruction {
            public final String args[];
            public final int type;

            private Instruction(int type, String... args) {
                this.type = type;
                this.args = args;
            }

            public static Instruction read(InputReader in) {
                String token = in.next();
                if (token.equals("end")) {
                    return new Instruction(4);
                }
                else if (token.equals("unlock")) {
                    return new Instruction(3);
                }
                else if (token.equals("lock")) {
                    return new Instruction(2);
                }
                else if (token.equals("print")) {
                    return new Instruction(1, in.next());
                }
                else {
                    String equal = in.next();
                    assert equal == "=";
                    return new Instruction(0, token, in.next());
                }
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
            int tests = in.nextInt();
            for (int test = 1; test <= tests; ++test) {
                long beg = System.nanoTime();
                new ConcurrencySimulator().process(test, in, out);
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

        public int nextInt() {
            return Integer.parseInt(next());
        }

        public String nextLine() {
            try {
                return reader.readLine();
            } catch (IOException e) {
                e.printStackTrace();
            }
            return null;
        }

        public void close() throws IOException {
            reader.close();
            reader = null;
        }
    }
}

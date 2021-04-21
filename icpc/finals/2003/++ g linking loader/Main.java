import java.io.*;
import java.util.*;

public class Main {
    private static class ExternalSymbol {
        public String name;
        public int defined;
        public int address;
        public int offset;

        public ExternalSymbol(String name) {
            this.name = name;
            this.offset = -1;
        }
        public ExternalSymbol(String name, int offset) {
            this.name = name;
            this.offset = offset;
        }
    }

    private static class ExternalLink {
        public int offset;
        public String name;

        public ExternalLink(int offset, String name) {
            this.offset = offset;
            this.name = name;
        }
    }

    private static class ObjectModule {
        public List<ExternalSymbol> externals;
        public List<ExternalLink> links;
        public List<Integer> code;
        public int address;

        public ObjectModule(List<ExternalSymbol> externals, List<ExternalLink> links, List<Integer> code) {
            this.externals = externals;
            this.links = links;
            this.code = code;
        }
    }

    private static class Program {
        private List<ObjectModule> modules;
        private SortedMap<String, ExternalSymbol> symbols;
        private int[] memory;
        private int size;

        private Program() {
            this.symbols = new TreeMap<String, ExternalSymbol>();
            this.modules = new ArrayList<ObjectModule>();
            this.size = 0x100;
        }

        private int checksum() {
            int result = 0;
            for (int i = 0; i < size; ++i) {
                int bi = (result >> 15) & 1;
                result = (result << 1) | bi;
                result = result & 0xFFFF;
                result = result + memory[i];
                result = result & 0xFFFF;
            }
            return result;
        }

        public void add(ObjectModule module) {
            module.address = size;
            size += module.code.size();
            modules.add(module);
            for (ExternalSymbol symbol : module.externals) {
                if (symbols.containsKey(symbol.name) == false) {
                    symbols.put(symbol.name, symbol);
                }
                ExternalSymbol extern = symbols.get(symbol.name);
                if (extern.defined == 0) {
                    if (symbol.offset >= 0) {
                        extern.address = module.address + symbol.offset;
                    }
                }
                if (symbol.offset >= 0) {
                    ++extern.defined;
                }
            }
        }

        public void link() {
            memory = new int[size];
            for (ObjectModule module : modules) {
                int address = module.address;
                List<Integer> code = module.code;
                for (int offset = 0; offset < code.size(); ++offset) {
                    memory[address + offset] = code.get(offset);
                }
                for (ExternalLink link : module.links) {
                    ExternalSymbol extern = symbols.get(link.name);
                    int linkAddress = address + link.offset;
                    memory[linkAddress + 0] = (extern.address >> 8) & 0xFF;
                    memory[linkAddress + 1] = (extern.address >> 0) & 0xFF;
                }
            }
        }

        public void dump(PrintWriter out) {
            out.printf("checksum = %04X\n", checksum());
            out.println(" SYMBOL   ADDR");
            out.println("--------  ----");
            for (String name : symbols.keySet()) {
                out.printf("%-10s", name);
                ExternalSymbol symbol = symbols.get(name);
                if (symbol.defined > 0) {
                    out.printf("%04X", symbol.address);
                    if (symbol.defined > 1) {
                        out.printf(" M");
                    }
                }
                else {
                    out.printf("????");
                }
                out.println();
            }
        }

        public static Program load(Scanner in) {
            char obj = in.next().charAt(0);
            if (obj == '$') {
                return null;
            }
            Program program = new Program();
            while (obj != '$') {
                List<ExternalSymbol> externals = new ArrayList<ExternalSymbol>();
                List<ExternalLink> links = new ArrayList<ExternalLink>();
                List<Integer> code = new ArrayList<Integer>();
                List<String> ref = new ArrayList<String>();
                do {
                    switch (obj) {
                        case 'D': {
                            String name = in.next();
                            String offset = in.next();
                            externals.add(new ExternalSymbol(name, Integer.parseInt(offset, 16)));
                            break;
                        }
                        case 'E': {
                            String name = in.next();
                            externals.add(new ExternalSymbol(name));
                            ref.add(name);
                            break;
                        }
                        case 'C': {
                            int numOfBytes = Integer.parseInt(in.next(), 16);
                            for (int i = 0; i < numOfBytes; ++i) {
                                String b = in.next();
                                if (b.equals("$")) {
                                    int link = Integer.parseInt(in.next(), 16);
                                    links.add(new ExternalLink(code.size(), ref.get(link)));
                                    code.add(0);
                                    code.add(0);
                                    i = i + 1;
                                } else {
                                    code.add(Integer.parseInt(b, 16));
                                }
                            }
                            break;
                        }
                    }
                    obj = in.next().charAt(0);
                } while (obj != 'Z');
                program.add(new ObjectModule(externals, links, code));

                obj = in.next().charAt(0);
            }
            program.link();
            return program;
        }
    }

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        PrintWriter out = new PrintWriter(System.out);
        for (int testCase = 1; true; ++testCase) {
            Program program = Program.load(in);
            if (program != null) {
                if (testCase > 1) {
                    out.println();
                }
                out.printf("Case %d: ", testCase);
                program.dump(out);
            }
            else {
                break;
            }
        }
        in.close();
        out.close();
    }
}

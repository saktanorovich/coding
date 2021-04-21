using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithm {
      public class DNAsynth {
            public int longest(string[] reactivity) {
                  List<Reaction> reactions = new List<Reaction>();
                  List<DNA> strands = new List<DNA>();
                  foreach (string reaction in reactivity) {
                        string[] seq = reaction.Split(new char[] { ':' });
                        string iprefix = seq[1];
                        string isuffix = seq[0];
                        string rprefix = reverse(isuffix);
                        string rsuffix = reverse(iprefix);
                        reactions.Add(new Reaction(iprefix, isuffix));
                        reactions.Add(new Reaction(rprefix, rsuffix));
                        strands.Add(new DNA(getPrefix(isuffix + iprefix), getSuffix(isuffix + iprefix), isuffix.Length + iprefix.Length));
                        strands.Add(new DNA(getPrefix(rsuffix + rprefix), getSuffix(rsuffix + rprefix), rsuffix.Length + rprefix.Length));
                        strands.Add(new DNA(iprefix, iprefix, iprefix.Length));
                        strands.Add(new DNA(isuffix, isuffix, isuffix.Length));
                        strands.Add(new DNA(rprefix, rprefix, rprefix.Length));
                        strands.Add(new DNA(rsuffix, rsuffix, rsuffix.Length));
                  }
                  Dictionary<Reaction, List<DNA>> prefixes = new Dictionary<Reaction, List<DNA>>();
                  Dictionary<Reaction, List<DNA>> suffixes = new Dictionary<Reaction, List<DNA>>();
                  foreach (Reaction reaction in reactions) {
                        prefixes[reaction] = new List<DNA>();
                        suffixes[reaction] = new List<DNA>();
                        foreach (DNA dna in strands) {
                              if (dna.prefix.StartsWith(reaction.prefix)) {
                                    prefixes[reaction].Add(dna);
                              }
                              if (dna.suffix.EndsWith(reaction.suffix)) {
                                    suffixes[reaction].Add(dna);
                              }
                        }
                  }
                  int result = 0;
                  PriorityQueue<DNA> queue = new PriorityQueue<DNA>(strands.ToArray());
                  while (queue.Count > 0) {
                        DNA curr = queue.Peek(); queue.Pop();
                        result = Math.Max(result, curr.length);
                        if (curr.length > 4 * strands.Count) {
                              return -1;
                        }
                        result = Math.Max(result, curr.length);
                        foreach (Reaction reaction in reactions) {
                              if (curr.suffix.EndsWith(reaction.suffix)) {
                                    foreach (DNA next in prefixes[reaction]) {
                                          relax(reaction, curr, next, queue);
                                    }
                              }
                              if (curr.prefix.StartsWith(reaction.prefix)) {
                                    foreach (DNA next in suffixes[reaction]) {
                                          relax(reaction, next, curr, queue);
                                    }
                              }
                        }
                  }
                  return result;
            }

            private readonly int[] maxLength = new int[256 * 256];

            private void relax(Reaction reaction, DNA dna1, DNA dna2, PriorityQueue<DNA> queue) {
                  DNA dna = dna1 + dna2;
                  if (maxLength[dna.GetHashCode()] < dna.length) {
                        maxLength[dna.GetHashCode()] = dna.length;
                        queue.Push(dna);
                  }
            }

            private static string getPrefix(string s) {
                  return s.Substring(0, 4);
            }

            private static string getSuffix(string s) {
                  return s.Substring(s.Length - 4, 4);
            }

            private static string reverse(string s) {
                  string result = string.Empty;
                  for (int i = 0; i < s.Length; ++i) {
                        result += s[s.Length - 1 - i];
                  }
                  return result;
            }

            public class Reaction {
                  public string prefix;
                  public string suffix;

                  public Reaction(string prefix, string suffix) {
                        this.prefix = prefix;
                        this.suffix = suffix;
                  }
            }

            public class DNA : IComparable<DNA> {
                  public string prefix;
                  public string suffix;
                  public int length;

                  public DNA(string prefix, string suffix, int length) {
                        this.prefix = prefix;
                        this.suffix = suffix;
                        this.length = length;
                  }

                  public override int GetHashCode() {
                        return (encode(prefix) << 8) + encode(suffix);
                  }

                  public override string ToString() {
                        if (length < 8) {
                              return prefix + suffix.Substring(suffix.Length - length + prefix.Length, length - prefix.Length);
                        }
                        return string.Format("{0}..{1}",  prefix, suffix);
                  }

                  public int CompareTo(DNA other) {
                        return -1 * this.length.CompareTo(other.length);
                  }

                  public static DNA operator +(DNA dna1, DNA dna2) {
                        string dna = dna1.ToString() + dna2.ToString();
                        return new DNA(getPrefix(dna),
                                          getSuffix(dna),
                                                dna1.length + dna2.length);
                  }

                  private static int encode(string dna) {
                        int result = 0;
                        for (int i = 0; i < dna.Length; ++i) {
                              result = (result << 2) + encode(dna[i]);
                        }
                        return result;
                  }

                  private static int encode(char nucleotide) {
                        switch (nucleotide) {
                              case 'A': return 0;
                              case 'C': return 1;
                              case 'T': return 2;
                              case 'G': return 3;
                        }
                        throw new Exception();
                  }
            }

            private class PriorityQueue<T> where T : IComparable<T> {
                  private Heap<T> heap;

                  public PriorityQueue(T[] elements) {
                        heap = new Heap<T>(elements);
                  }

                  public int Count {
                        get {
                              return heap.ElementsCount;
                        }
                  }

                  public void Push(T element) {
                        heap.Push(element);
                  }

                  public T Peek() {
                        if (heap.ElementsCount > 0) {
                              return heap[1].Element;
                        }
                        return default(T);
                  }

                  public void Pop() {
                        if (heap.ElementsCount > 0) {
                              heap.Pop(heap[1]);
                        }
                  }
            }

            private class HeapEntry<T> where T : IComparable<T> {
                  public T Element { get; set; }
                  public int Position { get; set; }

                  public HeapEntry(T element, int position) {
                        Element = element;
                        Position = position;
                  }
            }

            private class Heap<T> where T : IComparable<T> {
                  private static readonly int HeapGrowFactor = 65536;
                  private HeapEntry<T>[] container;
                  private int elementsCount;

                  public Heap(T[] elements) {
                        elementsCount = elements.Length;
                        container = new HeapEntry<T>[((elementsCount + HeapGrowFactor) / HeapGrowFactor) * HeapGrowFactor + 1];
                        for (int i = 1; i <= elementsCount; ++i) {
                              container[i] = new HeapEntry<T>(elements[i - 1], i);
                        }
                        for (int i = elementsCount / 2; i > 0; --i) {
                              PushDown(container[i]);
                        }
                  }

                  public int ElementsCount {
                        get {
                              return elementsCount;
                        }
                  }

                  public HeapEntry<T> this[int index] {
                        get {
                              if (1 <= index && index <= elementsCount) {
                                    return container[index];
                              }
                              return null;
                        }
                  }

                  public HeapEntry<T> Push(T element) {
                        if (elementsCount + 1 < container.Length) {
                              elementsCount = elementsCount + 1;
                              HeapEntry<T> heapEntry = new HeapEntry<T>(element, elementsCount);
                              container[elementsCount] = heapEntry;
                              PushUp(heapEntry);
                              return heapEntry;
                        }
                        Array.Resize(ref container, container.Length + HeapGrowFactor);
                        return Push(element);
                  }

                  public void Pop(HeapEntry<T> entry) {
                        if (entry.Position > 0) {
                              HeapEntry<T> rightist = container[elementsCount];
                              Swap(entry.Position, rightist.Position);
                              elementsCount = elementsCount - 1;
                              PushUp(rightist);
                              PushDown(rightist);
                        }
                  }

                  private void PushUp(HeapEntry<T> entry) {
                        if (entry.Position > 1) {
                              HeapEntry<T> parent = Parent(entry);
                              if (entry.Element.CompareTo(parent.Element) < 0) {
                                    Swap(entry.Position, parent.Position);
                                    PushUp(entry);
                              }
                        }
                  }

                  private void PushDown(HeapEntry<T> entry) {
                        HeapEntry<T> leChild = LeChild(entry);
                        HeapEntry<T> riChild = RiChild(entry);
                        if (leChild != null) {
                              HeapEntry<T> child = leChild;
                              if (riChild != null) {
                                    if (riChild.Element.CompareTo(leChild.Element) < 0) {
                                          child = riChild;
                                    }
                              }
                              if (child.Element.CompareTo(entry.Element) < 0) {
                                    Swap(entry.Position, child.Position);
                                    PushDown(entry);
                              }
                        }
                  }

                  private void Swap(int a, int b) {
                        container[a].Position = b;
                        container[b].Position = a;
                        HeapEntry<T> temporary = container[a];
                        container[a] = container[b];
                        container[b] = temporary;
                  }

                  private HeapEntry<T> Parent(HeapEntry<T> entry) {
                        if (1 < entry.Position && entry.Position <= elementsCount) {
                              return container[entry.Position / 2];
                        }
                        return null;
                  }

                  private HeapEntry<T> LeChild(HeapEntry<T> entry) {
                        if (2 * entry.Position <= elementsCount) {
                              return container[2 * entry.Position];
                        }
                        return null;
                  }

                  private HeapEntry<T> RiChild(HeapEntry<T> entry) {
                        if (2 * entry.Position + 1 <= elementsCount) {
                              return container[2 * entry.Position + 1];
                        }
                        return null;
                  }
            }
 
            internal static void Main(string[] args) {
                  Console.WriteLine(new DNAsynth().longest(new string[] { "TTA:AGG" }));
                  Console.WriteLine(new DNAsynth().longest(new string[] { "TTA:AGG", "AGG:CCC" }));
                  Console.WriteLine(new DNAsynth().longest(new string[] { "CCC:AAA", "AAA:CCC" }));
                  Console.WriteLine(new DNAsynth().longest(new string[] { "AG:AC", "AT:GC", "GC:AG" }));
                  Console.WriteLine(new DNAsynth().longest(new string[] { "TAG:ATC", "GCA:CCCT", "GAT:AC" }));
                  Console.WriteLine(new DNAsynth().longest(new string[] { "TG:GA", "CGGG:TGG", "GGA:AAAC" }));
                  Console.WriteLine(new DNAsynth().longest(new string[] { "CCCA:TGGG", "TGG:GGA", "GGGA:CCCA" }));

                  Console.WriteLine(new DNAsynth().longest(new string[] {
                        "CGTC:ATTG", "CTG:TTAC", "CTGC:AAG", "CAAG:TAC", "TC:TTAT", "AC:GATA", "GGC:TTA", "TAAA:CTAG", "CAT:ATCC", "AGG:CTT", "GA:TTCT", "ACC:TCTG",
                        "ATAT:CTAT", "CA:GT", "TGTA:TTA", "TTC:TAG", "AGG:TCC", "GC:CTT", "CG:TGC", "TCC:ATCA", "CGTA:TA", "TA:AG", "CTA:GGGA", "TC:TGC", "TTTC:GATT", "ATCG:GTC", "CTA:ACG", "CG:TGG",
                        "TGTT:TTTG", "GT:TTAC", "TAAA:TGCT", "GCTG:CGAA", "GCAA:TG", "TA:GTAT", "GT:CTCG" })); // -1
                  Console.WriteLine(new DNAsynth().longest(new string[] {
                        "TGCC:CT", "CA:CG", "TTG:ATT", "CG:CTA", "GCTA:GT", "CAAA:GCA",
                        "AACG:TA", "CA:TC", "ATTC:TA", "TGA:TTC", "GCAG:AG" })); // -1
                  Console.WriteLine(new DNAsynth().longest(new string[] {
                        "ACG:GCTG", "GG:GCC", "GCTC:GTAC", "CT:AC", "ACC:TA", "GAGC:CT", "AACG:TG", "TG:TCC", "CG:AGA", "GC:TCCG", "ACTT:CTG", "TAT:TC", "TG:GCGC", "CTTG:GCT",
                        "TTG:AA", "GCG:ATG", "AA:TC", "ATGG:TT", "CGCG:CAC", "ATGG:TCT", "AC:TCTC", "TTTC:CT", "TAT:TCGG", "CAT:GCG", "AGT:GA", "CT:GGC", "AGA:GTG", "TT:GGT",
                        "AGG:TCC", "CTG:AC", "AG:CTC", "CT:AA", "CGA:GCAC", "AAGG:AGT", "AG:GA", "GTAG:GAAG", "ACT:ATG", "CGCC:TT", "TAG:ATC", "GAG:ATCG", "ACC:CT", "TG:GAGT", "TCC:CGTC",
                        "ATC:TAA", "ACAA:TG", "TAGG:AT", "TC:CTTG", "CCTC:TCG", "GAAG:AGTC", "TCAC:CCC" })); // -1
                  Console.WriteLine(new DNAsynth().longest(new string[] {
                        "GCA:ACAG", "AACA:TCC", "AGC:TGAG", "CT:GCC", "GC:CGCC", "GTGT:GCA", "TAC:TGC", "GAGT:ATT", "TAC:CGG", "TTG:AGGC", "CCA:TA", "CT:GCA", "GGGA:AT", "ACG:TG", "AAT:TATA", "TGG:AT", "TA:AG", "TA:TAG", "CCTG:ACCC", "TGTA:ACGA", "CT:CCTT", "TTGA:TCAC", "AC:CTG",
                        "CGGA:GCA", "AT:TGC", "AAT:GAT", "CGGA:CA", "ATTC:AGAA", "GT:ATT", "CAG:AGG", "TTAT:TAC" })); // -1

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
     }

}
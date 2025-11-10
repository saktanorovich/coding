/**
Tieu owns a pizza restaurant and he manages it in his own way.
While in a normal restaurant, a customer is served by following
the first-come, first-served rule, Tieu simply minimizes the
average waiting time of his customers. So he gets to decide
who is served first, regardless of how sooner or later a person comes.

Different kinds of pizzas take different amounts of time to cook.
Also, once he starts cooking a pizza, he cannot cook another pizza
until the first pizza is completely cooked. Let's say we have three
customers who come at time t=0, t=1 and t=2 respectively, and the time
needed to cook their pizzas is 3, 9 and 6 respectively. If Tieu applies
first-come, first-served rule, then the waiting time of three customers
is 3, 11 and 16 respectively. The average waiting time in this case is
(3+11+16)/3=10. This is not an optimized solution. After serving the first
customer at time t=3, Tieu can choose to serve the third customer. In that case,
the waiting time will be 3, 7 and 17 respectively. Hence the average waiting
time is (3+7+17)/3=9.

Help Tieu achieve the minimum average waiting time. For the sake of simplicity,
just find the integer part of the minimum average waiting time.

Input
The first line contains an integer N, which is the number of customers.
In the next N lines, the ith line contains two space separated numbers
Ti and Li. Ti is the time when ith customer order a pizza, and Li is the
time required to cook that pizza.

Output
Display the integer part of the minimum average waiting time.

Constraints
1 ≤ N ≤ 100'000
0 ≤ Ti ≤ 1000'000'000
1 ≤ Li ≤ 1000'000'000

Note
  * The waiting time is calculated as the difference between the time a
    customer orders pizza (the time at which they enter the shop) and
    the time she is served.
  * Cook does not know about the future orders.

Sample Input
3
0 3
1 9
2 6
Sample Output
9

Sample Input
3
0 3
1 9
2 5

Sample Output
8

Explanation
Let's call the person ordering at time = 0 as A, time = 1 as B and time = 2 as C.
By delivering pizza for A, C and B we get the minimum average wait time to be
(3+6+16)/3=25/3=8.33 the integer part is 8 and hence the answer.
*/
using System;
using System.Collections.Generic;
using System.IO;

namespace interview.hackerrank {
    public class MinimumAverageWaitingTime {
        public long waitingTime(TextReader reader) {
            var n = Int32.Parse(reader.ReadLine());
            var enterTime = new int[n];
            var pizzaTime = new int[n];
            for (var i = 0; i < n; ++i) {
                var buf = reader.ReadLine().Split(' ');
                enterTime[i] = int.Parse(buf[0]);
                pizzaTime[i] = int.Parse(buf[1]);
            }
            return waitingTime(enterTime, pizzaTime);
        }

        public long waitingTime(int[] enterTime, int[] pizzaTime) {
            Array.Sort(enterTime, pizzaTime);
            var customers = new List<Customer>();
            for (var i = 0; i < enterTime.Length; ++i) {
                customers.Add(new Customer(enterTime[i], pizzaTime[i]));
            }
            customers.Add(new Customer(long.MaxValue, 0));
            var queue = new PriorityQueue<Customer>((a, b) => {
                return a.PizzaTime.CompareTo(b.PizzaTime);
            });
            long wait = 0, time = 0;
            for (int index = 0, processed = enterTime.Length; processed > 0;) {
                while (customers[index].EnterTime <= time) {
                    queue.Push(customers[index]);
                    index = index + 1;
                }
                if (queue.Count > 0) {
                    var cook = queue.Pop();
                    time += cook.PizzaTime;
                    wait += time - cook.EnterTime;
                    processed = processed - 1;
                }
                else {
                    time = customers[index].EnterTime;
                }
            }
            return wait / enterTime.Length;
        }

        private class Customer {
            public long EnterTime { get; private set; }
            public long PizzaTime { get; private set; }

            public Customer(long enterTime, long pizzaTime) {
                EnterTime = enterTime;
                PizzaTime = pizzaTime;
            }
        }

        private class PriorityQueue<T> {
            private readonly Heap<T> heap;

            public PriorityQueue(Comparison<T> comparison) {
                heap = new Heap<T>(comparison);
            }

            public int Count {
                get { return heap.ElementsCount; }
            }

            public void Push(T element) {
                heap.Push(element);
            }

            public T Pop() {
                if (heap.ElementsCount > 0) {
                    var top = heap[1];
                    heap.Pop(top);
                    return top.Element;
                }
                throw new InvalidOperationException();
            }
        }

        private class Heap<T> {
            private static readonly int HeapGrowFactor = 65536;

            private readonly Comparison<HeapEntry<T>> compare;
            private HeapEntry<T>[] container;

            public Heap(Comparison<T> comparison) {
                container = new HeapEntry<T>[HeapGrowFactor + 1];
                compare = (a, b) => comparison(a.Element, b.Element);
                ElementsCount = 0;
            }

            public int ElementsCount { get; private set; }

            public HeapEntry<T> this[int index] {
                get {
                    if (1 <= index && index <= ElementsCount) {
                        return container[index];
                    }
                    return null;
                }
            }

            public HeapEntry<T> Push(T element) {
                if (ElementsCount + 1 < container.Length) {
                    ElementsCount = ElementsCount + 1;
                    var heapEntry = new HeapEntry<T>(element, ElementsCount);
                    container[ElementsCount] = heapEntry;
                    PushUp(heapEntry);
                    return heapEntry;
                }
                Array.Resize(ref container, container.Length + HeapGrowFactor);
                return Push(element);
            }

            public void Pop(HeapEntry<T> entry) {
                if (entry.Position > 0) {
                    var rightist = container[ElementsCount];
                    Swap(entry.Position, rightist.Position);
                    container[ElementsCount] = null;
                    ElementsCount = ElementsCount - 1;
                    PushUp(rightist);
                    PushDown(rightist);
                }
            }

            private void PushUp(HeapEntry<T> entry) {
                if (entry.Position > 1) {
                    var parent = Parent(entry);
                    if (compare(entry, parent) < 0) {
                        Swap(entry.Position, parent.Position);
                        PushUp(entry);
                    }
                }
            }

            private void PushDown(HeapEntry<T> entry) {
                var leChild = LeChild(entry);
                var riChild = RiChild(entry);
                if (leChild != null) {
                    var child = leChild;
                    if (riChild != null) {
                        if (compare(riChild, leChild) < 0) {
                            child = riChild;
                        }
                    }
                    if (compare(child, entry) < 0) {
                        Swap(entry.Position, child.Position);
                        PushDown(entry);
                    }
                }
            }

            private void Swap(int a, int b) {
                container[a].Position = b;
                container[b].Position = a;
                var temporary = container[a];
                container[a] = container[b];
                container[b] = temporary;
            }

            private HeapEntry<T> Parent(HeapEntry<T> entry) {
                if (1 < entry.Position && entry.Position <= ElementsCount) {
                    return container[entry.Position / 2];
                }
                return null;
            }

            private HeapEntry<T> LeChild(HeapEntry<T> entry) {
                if (2 * entry.Position <= ElementsCount) {
                    return container[2 * entry.Position];
                }
                return null;
            }

            private HeapEntry<T> RiChild(HeapEntry<T> entry) {
                if (2 * entry.Position + 1 <= ElementsCount) {
                    return container[2 * entry.Position + 1];
                }
                return null;
            }
        }

        private class HeapEntry<T> {
            public T Element { get; private set; }
            public int Position { get; set; }

            public HeapEntry(T element, int position) {
                Element = element;
                Position = position;
            }
        }
    }
}

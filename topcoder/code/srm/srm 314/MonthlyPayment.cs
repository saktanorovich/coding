using System;
using System.Collections.Generic;

public class MonthlyPayment {
      private const int paymentPerSMS = 10;
      private const int criticalMessagesCount = 1000000;

      private struct Package {
            public long messagesCount;
            public long payment;

            public Package(string messagesCount, string payment) {
                  this.messagesCount = long.Parse(messagesCount);
                  this.payment = long.Parse(payment);
            }
      }

      public long minimalPayment(string totalSMS, string pack1, string pay1, string pack2, string pay2) {
            return minimalPayment(long.Parse(totalSMS), new Package(pack1, pay1), new Package(pack2, pay2));
      }

      private long minimalPayment(long total, Package pack1, Package pack2) {
            if (pack2.messagesCount > pack1.messagesCount) {
                  return minimalPayment(total, pack2, pack1);
            }
            else {
                  long lo = 0, hi = (total / pack1.messagesCount) + 1;
                  while (lo + criticalMessagesCount < hi) {
                        long lothird = (2 * lo + hi) / 3;
                        long hithird = (lo + 2 * hi) / 3;

                        long lototal = lothird * pack1.payment + minimalPayment(total - lothird * pack1.messagesCount, pack2);
                        long hitotal = hithird * pack1.payment + minimalPayment(total - hithird * pack1.messagesCount, pack2);
                        if (lototal < hitotal) {
                              hi = hithird;
                        }
                        else {
                              lo = lothird;
                        }
                  }
                  long result = total * paymentPerSMS;
                  for (long i = lo; i <= hi; ++i) {
                        result = Math.Min(result, i * pack1.payment + minimalPayment(total - i * pack1.messagesCount, pack2));
                  }
                  return result;
            }
      }

      private long minimalPayment(long total, Package pack) {
            if (total > 0) {
                  long result = total * paymentPerSMS;

                  long count = (total / pack.messagesCount) + 1;
                  for (long i = count - 1; i <= count + 1; ++i) {
                        result = Math.Min(result, i * pack.payment + Math.Max(0, total - i * pack.messagesCount) * paymentPerSMS);
                  }
                  return result;
            }
            return 0;
      }

      static void Main(string[] args) {
            Console.WriteLine(new MonthlyPayment().minimalPayment("92", "10", "90", "20", "170"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("90", "10", "90", "20", "170"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("99", "10", "90", "20", "170"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("10", "1", "11", "20", "300"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("0", "10", "80", "50", "400"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("28", "1", "10", "1", "8"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("450702146848", "63791", "433956", "115281", "666125"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("45", "6", "12", "7", "14"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("1", "10", "1", "50", "2"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("1000000000000", "1", "1", "2", "2"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("574746364312", "349", "4120", "937", "11049"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("43253205504", "26145712030", "131147554904", "29748828881", "241386341620"));
            Console.WriteLine(new MonthlyPayment().minimalPayment("999999999999", "10", "60", "99999999999", "599999999995"));
            Console.ReadKey();
      }
}

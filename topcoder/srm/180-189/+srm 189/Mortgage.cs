using System;

namespace TopCoder.Algorithm {
    public class Mortgage {
        public int monthlyPayment(int loan, int interest, int term) {
            int lo = 1, hi = loan;
            while (lo < hi) {
                var payment = lo + (hi - lo) / 2;
                if (enough(loan, interest, term, payment))
                    hi = payment;
                else
                    lo = payment + 1;
            }
            return lo;
        }

        private bool enough(int loan, int interest, int term, int payment) {
            for (var month = 1; month <= 12 * term; ++month) {
                loan = Math.Max(0, loan - payment);
                if (loan == 0) {
                    return true;
                }
                loan = (int)Math.Ceiling(loan * (1 + 1.0 * interest / 10 / 100 / 12));
            }
            return loan == 0;
        }
    }
}
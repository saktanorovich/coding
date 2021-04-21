using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class BettingMoney {
        public int moneyMade(int[] amounts, int[] centsPerDollar, int finalResult) {
            return (amounts.Sum() - amounts[finalResult]) * 100 - amounts[finalResult] * centsPerDollar[finalResult];
        }
    }
}
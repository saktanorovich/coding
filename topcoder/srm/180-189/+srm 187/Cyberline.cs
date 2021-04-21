using System;
using System.Linq;

namespace TopCoder.Algorithm {
    public class Cyberline {
        public string lastCyberword(string cyberline) {
            return cyberline.Replace("-", string.Empty)
                .Split(Delimeters.ToArray(), StringSplitOptions.RemoveEmptyEntries).Last();
        }

        private const string Delimeters = ".,()?!<>#&$%_=+;:/~|{}- ";
    }
}
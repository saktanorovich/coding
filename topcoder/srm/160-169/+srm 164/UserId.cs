using System.Text.RegularExpressions;

namespace TopCoder.Algorithm {
      public class UserId {
          public string id(string[] inUse, string fi, string mi, string la) {
              fi = eleminate(fi);
              mi = eleminate(mi);
              la = eleminate(la);
              if (isValid(fi) &&
                  isValid(mi) &&
                  isValid(la)) {
                    if (fi.Length > 1 && la.Length > 1) {
                        return idImpl(inUse, fi.ToLower(), mi.ToLower(), la.ToLower());
                    }
              }
              return "BAD DATA";
          }

          private string idImpl(string[] inUse, string fi, string mi, string la) {
              string id = rule1(fi, mi, la);
              if (!contains(inUse, id)) {
                  return id;
              }
              id = rule2(fi, mi, la);
              if (!contains(inUse, id)) {
                return id;
              }
              for (int i = 1; i < 100; ++i) {
                  id = rule3(fi, mi, la, i);
                  if (!contains(inUse, id)) {
                      return id;
                  }
              }
              return null;
          }

          private static string rule1(string fi, string mi, string la) {
              string result = string.Format("{0}{1}", fi[0], la);
              if (result.Length > 8) {
                  result = result.Substring(0, 8);
              }
              return result;
          }

          private static string rule2(string fi, string mi, string la) {
              if (!string.IsNullOrEmpty(mi)) {
                  string result = string.Format("{0}{1}{2}", fi[0], mi[0], la);
                  if (result.Length > 8) {
                      result = result.Substring(0, 8);
                  }
                  return result;
              }
              return null;
          }

          private static string rule3(string fi, string mi, string la, object context) {
              string result = string.Format("{0}{1}", fi[0], la);
              if (result.Length > 6) {
                  result = result.Substring(0, 6);
              }
              return result + context.ToString().PadLeft(2, '0');
          }

          private static bool contains(string[] a, string s) {
              if (!string.IsNullOrEmpty(s)) {
                  for (int i = 0; i < a.Length; ++i) {
                      if (a[i] == s) {
                          return true;
                      }
                  }
                  return false;
              }
              return true;
          }

          private static string eleminate(string name) {
              return name.Replace(" ", string.Empty)
                         .Replace("'", string.Empty);
          }

          private static bool isValid(string name) {
              var validator = new Regex(@"^[a-zA-Z]*$");
              if (validator.IsMatch(name)) {
                  return true;
              }
              return false;
          }
      }
}
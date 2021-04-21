using System.Text;

class Solution {
  public string getWrongAnswers(int N, string C) {
    var res = new StringBuilder();
    foreach (var c in C) {
      res.Append(c == 'A' ? 'B' : 'A');
    }
    return res.ToString();
  }
}

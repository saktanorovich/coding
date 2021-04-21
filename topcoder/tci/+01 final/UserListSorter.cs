using System;
using System.Collections;
using System.Collections.Generic;

namespace TopCoder.Algorithms {
      public class UserListSorter {
            public string[] sortUsersBy(string[] userInfo, int sortBy, int order) {
                  List<UserInfo> users = new List<UserInfo>();
                  for (int i = 0; i < userInfo.Length; ++i) {
                        users.Add(UserInfo.Parse(i, userInfo[i]));
                  }
                  users.Sort(getComparison(sortBy, order));
                  return Array.ConvertAll(users.ToArray(), delegate(UserInfo user) {
                        return user.Handle;
                  });
            }

            private Comparison<UserInfo> getComparison(int sortBy, int order) {
                  order = 2 * order - 1;
                  return delegate(UserInfo a, UserInfo b) {
                        int res = 0;
                        switch (sortBy) {
                              case 1: res = order * a.Handle.CompareTo(b.Handle); break;
                              case 2: res = order * a.Rating.CompareTo(b.Rating); break;
                              case 3: res = order * a.LoginTime.CompareTo(b.LoginTime); break;
                        }
                        return (res != 0 ? res : a.Seqno.CompareTo(b.Seqno));
                  };
            }

            private class UserInfo {
                  public int Seqno;
                  public string Handle;
                  public int Rating;
                  public DateTime LoginTime;

                  public UserInfo(int seqno, string userName, int rating, DateTime loginTime) {
                        Seqno = seqno;
                        Handle = userName;
                        Rating = rating;
                        LoginTime = loginTime;
                  }

                  public static UserInfo Parse(int seqno, string descriptor) {
                        string[] s = descriptor.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        return new UserInfo(seqno, s[0], int.Parse(s[1]), DateTime.Parse(s[2]));
                  }
            }

            private static string ToString(string[] a) {
                  string result = string.Empty;
                  for (int i = 0; i < a.Length; ++i) {
                        result += a[i];
                        if (i + 1 < a.Length) {
                              result += ' ';
                        }
                  }
                  return result + Environment.NewLine;
            }

            public static void Main(string[] args) {
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"aaa 1500 10:30", "a 728 10:30", "aa 2898 14:32", }, 1, 1)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"dwarfsleepy 728 10:30", "mitalub 2898 14:32", "ads 1500 10:30", "mike 1727 22:00", "romana 1200 09:00", "td 1299 01:00", "dok 1200 17:25", "chuck 1200 18:00" }, 1, 1)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"dwarfsleepy 728 10:30", "mitalub 2898 14:32", "ads 1500 10:30", "mike 1727 22:00", "romana 1200 09:00", "td 1299 01:00", "dok 1200 17:25", "chuck 1200 18:00"}, 1, 1)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"dwarfsleepy 728 10:30", "mitalub 2898 14:32", "ads 1500 10:30", "mike 1727 22:00", "romana 1200 09:00", "td 1299 01:00", "dok 1200 17:25", "chuck 1200 18:00"}, 1, 0)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"dwarfsleepy 728 10:30", "mitalub 2898 14:32", "ads 1500 10:30", "mike 1727 22:00", "romana 1200 09:00", "td 1299 01:00", "dok 1200 17:25", "chuck 1200 18:00"}, 2, 1)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"dwarfsleepy 728 10:30", "mitalub 2898 14:32", "ads 1500 10:30", "mike 1727 22:00", "romana 1200 09:00", "td 1299 01:00", "dok 1200 17:25", "chuck 1200 18:00"}, 2, 0)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"dwarfsleepy 728 10:30", "mitalub 2898 14:32", "ads 1500 10:30", "mike 1727 22:00", "romana 1200 09:00", "td 1299 01:00", "dok 1200 17:25", "chuck 1200 18:00"}, 3, 1)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"dwarfsleepy 728 10:30", "mitalub 2898 14:32", "ads 1500 10:30", "mike 1727 22:00", "romana 1200 09:00", "td 1299 01:00", "dok 1200 17:25", "chuck 1200 18:00"}, 3, 0)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"sdfas 34 01:03", "asllsdf 23 03:28", "owkkdsf 13 04:12", "aslwl 1 03:11", "lajwpod 3 16:23", "lalilsd 342 12:59"}, 3, 0)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"aaaaaaaaaaaaaaaaaaaa 34 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03", "a 23 01:03"}, 3, 0)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"sdfas 34 01:03"}, 3, 0)));
                  Console.WriteLine(ToString(new UserListSorter().sortUsersBy(new string[] {"a 0000030 00:00", "b 000004 00:00" }, 2, 1)));

                  Console.WriteLine("Press any key to continue...");
                  Console.ReadLine();
            }
      }
}
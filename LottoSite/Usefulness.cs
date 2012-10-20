using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Rf.Lotto
{
  public static class Usefulness
  {
    public static string AsString(this int[] numbers)
    {
      return string.Join(",", numbers.OrderBy(i => i).Select(i => i.ToString()));
    }

      public static DateTime AsDateTime(this string s)
      {
          return DateTime.ParseExact(s, "dd.MM.yyyy", CultureInfo.InvariantCulture);
      }

      public static IEnumerable<string> Enumerate(this StreamReader reader)
      {
          while (!reader.EndOfStream)
              yield return reader.ReadLine();
      }
  }
}
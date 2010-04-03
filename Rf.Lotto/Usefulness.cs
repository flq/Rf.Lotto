using System.Linq;

namespace Rf.Lotto
{
  public static class Usefulness
  {
    public static string AsString(this int[] numbers)
    {
      return string.Join(",", numbers.OrderBy(i => i).Select(i => i.ToString()).ToArray());
    }
  }
}
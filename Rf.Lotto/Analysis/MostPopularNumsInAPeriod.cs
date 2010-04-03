using System;
using System.Linq;

namespace Rf.Lotto.Analysis
{
  public class MostPopularNumsInAPeriod : IAnalysis
  {
    public void Analyze(Drawing[] drawings)
    {
      Console.Write("Min and maxdate as dd.mm.yyyy, comma separated: ");
      string input = Console.ReadLine();
      var parts = input.Split(',');
      var minDate = DateTime.Parse(parts[0]);
      var maxDate = DateTime.Parse(parts[1]);

      var result = from d in drawings
                   where d.DayOfDraw >= minDate && d.DayOfDraw <= maxDate
                   from n in d
                   group d by n
                   into numbers
                     orderby numbers.Count() descending
                     select new {Number = numbers.Key, Hits = numbers.Count()};
      foreach (var d in result)
        Console.WriteLine("{0} drawn {1} times", d.Number, d.Hits);
    }
  }
}
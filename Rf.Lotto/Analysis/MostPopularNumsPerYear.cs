using System;
using System.Linq;

namespace Rf.Lotto.Analysis
{
  public class MostPopularNumsPerYear : IAnalysis
  {
    public void Analyze(Drawing[] drawings)
    {
      var result =
        from d in drawings
        group d by d.DayOfDraw.Year
        into years
          select new
                   {
                     Year = years.Key,
                     Numbers =
                      (from d in years
                      from n in d
                      group d by n into numbers
                      orderby numbers.Count() descending
                        select new { Number = numbers.Key, Hits = numbers.Count() })
                        .Take(10)
                   };

      foreach (var a in result)
      {
        Console.WriteLine("In the year {0} the most popular numbers were", a.Year);
        foreach (var n in a.Numbers)
          Console.WriteLine("{0,2} : {1} hits", n.Number, n.Hits);
      }

    }
  }
}
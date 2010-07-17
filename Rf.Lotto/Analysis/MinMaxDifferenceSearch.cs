using System;
using System.Linq;

namespace Rf.Lotto.Analysis
{
  public class MinMaxDifferenceSearch : IAnalysis
  {
    public void Analyze(Drawing[] drawings)
    {
      Console.Write("Enter the min-max difference:");
      var minMaxDiff = Console.ReadLine();
      var mDiff = int.Parse(minMaxDiff);
      var result =
        from d in drawings
        let smallest = d.Numbers.Min()
        let biggest = d.Numbers.Max()
        where biggest - smallest <= mDiff
        select d;

      foreach (var d in result)
        Console.WriteLine("{0}: {1}", d.DayOfDraw, d.Numbers.AsString());
    }
  }
}
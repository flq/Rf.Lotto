using System;
using System.Linq;

namespace Rf.Lotto.Analysis
{
  public class HowOftenWasANumberPulled : IAnalysis
  {
    public void Analyze(Drawing[] drawings)
    {
      var result = from d in drawings
                   from n in d
                   group d by n
                   into numbers
                     orderby numbers.Count() descending 
                     select numbers;
      foreach (var a in result)
        Console.WriteLine("{0,2}: {1} times", a.Key, a.Count());

    }
  }
}
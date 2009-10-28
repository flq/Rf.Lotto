using System;
using System.Linq;

namespace Rf.Lotto.Analysis
{
  public class SanityCheckAllNumbers6 : IAnalysis
  {
    public void Analyze(Drawing[] drawings)
    {
      var result = from d in drawings
                   group d by d.Numbers.Count()
                   into g select g;
      
      foreach (var a in result)
        Console.WriteLine("{0} entries have {1} numbers", a.Count(), a.Key);
      
    }
  }
}
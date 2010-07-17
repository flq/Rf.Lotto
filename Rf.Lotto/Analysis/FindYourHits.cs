using System;
using System.Linq;

namespace Rf.Lotto.Analysis
{
  public class FindYourHits : IAnalysis
  {
    public void Analyze(Drawing[] drawings)
    {
      Console.Write("Enter your numbers:");
      string s = Console.ReadLine();
      var nums = (from sP in s.Split(',')
                  select Int32.Parse(sP)).ToArray();

      var result =
        from drawing in drawings
        let matches = drawing.Where(d => nums.Contains(d))
        where matches.Count() > 2
        let matchedNums = 
          string.Join(",",
                      matches.OrderBy(i => i).Select(i => i.ToString()).ToArray())
        let elem = new {matches, matchedNums, drawing.DayOfDraw}
        group elem by elem.matchedNums
        into m
          orderby m.Key.Length descending
          let date = m.Count() > 1 ? 
            "Various" : m.FirstOrDefault().DayOfDraw.ToString("dd.MM.yyyy")
          select new
                   {
                     Numbers = "[" + m.Key + "]",
                     Hits = m.Count(),
                     DrawDate = date
                   };

      foreach (var a in result)
        Console.WriteLine("Numbers: {0} were drawn {1} times on {2}", a.Numbers, a.Hits, a.DrawDate);

    }
  }
}
using System.Linq;
using Raven.Client.Indexes;

namespace LottoSite.Indexes
{

    public class NumberCount_ByYear : AbstractIndexCreationTask<Drawing,NumberCountByYearResult>
    {
        public NumberCount_ByYear()
        {
            Map = drawings => from d in drawings
                              from number in d.Numbers
                              select new
                                     {
                                         d.DayOfDraw.Year,
                                         Number = number,
                                         Count = 1
                                     };
            Reduce = results => from r in results
                                group r by r.Number
                                into numbers
                                select new
                                       {
                                           numbers.First().Year,
                                           Number = numbers.Key,
                                           Count = numbers.Count()
                                       };
        }
    }

    public class NumberCountByYearResult
    {
        public int Year { get; set; }
        public int Number { get; set; }
        public int Count { get; set; }
    }
}
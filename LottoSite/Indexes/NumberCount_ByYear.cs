using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace LottoSite.Indexes
{

    public class NumberCount_ByYear : AbstractIndexCreationTask<Drawing,NumberCountByYearResult>
    {
        public NumberCount_ByYear()
        {
            Map = drawings =>
                  from d in drawings
                  from number in d.Numbers
                  select new
                    {
                        d.DayOfDraw.Year,
                        Number = number,
                        Count = 1
                    };
            Reduce = results =>
                     from r in results
                     group r by new {r.Year, r.Number}
                     into yearAndNumber
                     select new
                    {
                        yearAndNumber.Key.Year,
                        yearAndNumber.Key.Number,
                        Count = yearAndNumber.Sum(_ => _.Count)
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
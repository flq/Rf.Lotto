using System.Collections;
using System.Collections.Generic;
using LottoSite.Indexes;

namespace LottoSite
{

    public class CountByYearModel : IEnumerable<NumberCountByYearResult>
    {
        private readonly IEnumerable<NumberCountByYearResult> _results;

        public string Year { get; private set; }

        public CountByYearModel(string year, IEnumerable<NumberCountByYearResult> results)
        {
            _results = results;
            Year = year;
        }

        public IEnumerator<NumberCountByYearResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
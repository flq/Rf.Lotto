using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rf.Lotto;

namespace LottoSite
{
    public class Drawing : IEnumerable<int>
    {
        public DateTime DayOfDraw { get; set; }
        public int[] Numbers { get; set; }

        public Drawing() { }

        public Drawing(string data)
        {
            var parts = data.Split(',');
            DayOfDraw = parts[0].AsDateTime();
            Numbers = (from d in parts.Skip(1)
                       where d.Length > 0
                       select Int32.Parse(d)).ToArray();
        }

        public IEnumerator<int> GetEnumerator()
        {
            return Numbers.Cast<int>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Write(TextWriter writer)
        {
            writer.WriteLine(DayOfDraw.ToString("dd.MM.yyyy") + ","
                             + string.Join(",", Numbers.Select(n => n.ToString().PadLeft(2)).ToArray()));
        }

        public override string ToString()
        {
            return "[" + string.Join(",", Numbers.Select(i => i.ToString()).ToArray()) + "]";
        }
    }
}
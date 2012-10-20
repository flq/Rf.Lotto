using System;
using System.IO;
using Nancy;
using System.Linq;
using Raven.Client;
using Raven.Client.Exceptions;
using Rf.Lotto;

namespace LottoSite
{
    /// <summary>
    /// Main Nancy module
    /// </summary>
    /// <author>flq</author>
    /// <initialCreationDate>16.10.2012</initialCreationDate>
    public class MainModule : NancyModule
    {
        private readonly IDocumentStore _store;

        public MainModule(IDocumentStore store)
        {
            _store = store;
            Get["/"] = parameters => View["Index"];

            Get["/upload"] = parameters => View["Upload"];

            Post["/upload"] = parameters =>
            {
                var file = Request.Files.FirstOrDefault();
                if (file != null)
                {
                    ViewBag.DataCount = ImportFile(file.Value);
                    ViewBag.Processed = true;
                }
                
                return View["Upload"];
            };
        }

        private int ImportFile(Stream data)
        {
            using (var s = _store.OpenSession())
            {
                var count = s.Query<Drawing>().Count();
                var lastDate = count == 0 ? 
                    DateTime.MinValue : 
                    s.Query<Drawing>().OrderByDescending(d => d.DayOfDraw).FirstOrDefault().DayOfDraw;

                using (var f = new StreamReader(data))
                {
                    var parsedData = f.Enumerate()
                        .Select(l => new Drawing(l))
                        .TakeWhile(d => d.DayOfDraw > lastDate)
                        .ToList();

                    foreach (var d in parsedData)
                    {
                        try
                        {
                            s.Store(d, d.DayOfDraw.ToString("yyyyMMdd"));
                        }
                        catch (NonUniqueObjectException)
                        {
                            //I don't expect more than 2 drawings on a day in the set.
                            s.Store(d, d.DayOfDraw.ToString("yyyyMMdd") + "-2");
                        }
                    }

                    s.SaveChanges();

                    return parsedData.Count;
                }
            }
        }
    }
}
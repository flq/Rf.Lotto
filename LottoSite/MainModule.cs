﻿using System.Collections.Generic;
using LottoSite.Indexes;
using Nancy;
using System.Linq;
using Raven.Client;

namespace LottoSite
{

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
                    ViewBag.DataCount = new Importer(store).ImportFile(file.Value);
                    ViewBag.Processed = true;
                }
                
                return View["Upload"];
            };

            Get["/count_by_year"] = parameters =>
            {
                var numbers = GetNumbers(Request.Query.Year);
                return View["CountByYear", new CountByYearModel(Request.Query.Year, numbers)];
            };
        }

        private IEnumerable<NumberCountByYearResult> GetNumbers(int year)
        {
            using (var s = _store.OpenSession())
            {
                return s.Query<NumberCountByYearResult, NumberCount_ByYear>()
                    .Customize(c => c.WaitForNonStaleResults())
                    .Where(r => r.Year == year)
                    .OrderByDescending(r => r.Count)
                    .ToList();
            }
        }

        
    }
}
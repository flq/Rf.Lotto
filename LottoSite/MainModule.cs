using System.Diagnostics;
using System.IO;
using Nancy;
using System.Linq;
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
        public MainModule()
        {
            Get["/"] = parameters => View["Index"];

            Get["/upload"] = parameters => View["Upload"];

            Post["/upload"] = parameters =>
            {
                var file = Request.Files.FirstOrDefault();
                if (file != null)
                {
                    ViewBag.DataCount = ProcessFile(file.Value);
                    ViewBag.Processed = true;
                }
                
                return View["Upload"];
            };
        }

        private int ProcessFile(Stream data)
        {
            using (var f = new StreamReader(data))
            {
                var parsedData = f.Enumerate().Select(l => new Drawing(l)).ToArray();
                return parsedData.Length;
            }
        }
    }
}
using System;
using Raven.Client;
using Raven.Client.Embedded;

namespace RavenStudioStarter
{
    class Program
    {
        private static IDocumentStore _store;

        static void Main(string[] args)
        {
            _store = new EmbeddableDocumentStore
                     {
                         DataDirectory =
                             @"C:\Users\flq\Documents\github\Rf.Lotto\LottoSite\App_Data\Database",
                         UseEmbeddedHttpServer = true
                     };
            _store.Initialize();
            Console.WriteLine("hanging there...");
            Console.ReadLine();
            _store.Dispose();
        }
    }
}

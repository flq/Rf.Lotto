using System;
using Raven.Client;
using Raven.Client.Embedded;

namespace LottoSite
{
    public class DocumentStoreContainer
    {
        private static IDocumentStore _instance;

        public static IDocumentStore Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("IDocumentStore has not been initialized.");
                return _instance;
            }
        }

        public static IDocumentStore Initialize()
        {
            _instance = new EmbeddableDocumentStore
                        {
                            ConnectionStringName = "RavenDB"
                        };
            _instance.Initialize();
            Raven.Client.Indexes.IndexCreation.CreateIndexes(typeof (DocumentStoreContainer).Assembly, _instance);
            return _instance;
        }
    }
}
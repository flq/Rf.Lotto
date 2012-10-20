using System.Collections.Specialized;
using System.Configuration;
using Nancy;
using Raven.Client;

namespace LottoSite
{
    public class Bootstrapping : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoC.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(typeof(NameValueCollection), ConfigurationManager.AppSettings);
            DocumentStoreContainer.Initialize();
            container.Register(DocumentStoreContainer.Instance);
            container.Register((ioc, _) => ioc.Resolve<IDocumentStore>().OpenSession());
        }
    }
}
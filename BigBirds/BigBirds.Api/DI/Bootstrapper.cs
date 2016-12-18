using Autofac;
using Autofac.Integration.WebApi;
using BigBirds.Api.Utils;
using BigBirds.Domain.Entities;
using BigBirds.Domain.Repository;
using BigBirds.Services;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace BigBirds.Api.DI
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            // registrar os controllers do assembly
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // registrar os filtros
            builder.RegisterWebApiFilterProvider(config);

            // registrar os servicos da aplicacao
            builder.Register<BigBirds.Domain.Contexts.BigBirdsContext>(o => new Domain.Contexts.BigBirdsContext());
            builder.RegisterType<ConclusaoReclamacaoRepository>().As<IConclusaoReclamacaoRepository>();
            builder.RegisterType<RespostasAnterioresService>().As<IRespostasAnterioresService>();
            builder.RegisterType<CategoriaRepository>().As<IRepository<Categoria>>();
            builder.RegisterType<OrgaoRepository>().As<IRepository<Orgao>>();
            builder.RegisterType<OrgaoRepository>().As<IOrgaoRepository>();
            builder.Register<ORChatService>(o => new ORChatService(ConfigConstants.WIT_ACCESS_TOKEN, new OrgaoRepository())).As<IORChatService>();
            

            IContainer container = builder.Build();

            var diResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = diResolver;

            //DependencyResolver.SetResolver(diResolver);
        }
    }
}
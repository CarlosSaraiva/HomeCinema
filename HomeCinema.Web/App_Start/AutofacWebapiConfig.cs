using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using HomeCinema.Data;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Services;

namespace HomeCinema.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //EF HomeCinemaContext
            builder.RegisterType<HomeCinemaContext>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<UnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(EntityBaseRepository<>)).As(typeof(IEntityBaseRepository<>)).InstancePerRequest();

            //Services
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerRequest();
            Container = builder.Build();

            return Container;
        }
    }
}
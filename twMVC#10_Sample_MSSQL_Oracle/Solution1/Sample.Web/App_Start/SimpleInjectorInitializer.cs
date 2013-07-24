[assembly: WebActivator.PostApplicationStartMethod(typeof(Sample.Web.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace Sample.Web.App_Start
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using Sample.Domain.Utilities;
    using Sample.Repository.Interface;
    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: http://bit.ly/YE8OJj.
            var container = new Container();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.RegisterMvcAttributeFilterProvider();
       
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            //=====================================================================================
            // Register Type for Repository

            string repositoryType = MvcApplication.RepositoryType;

            string repositoryAssemblyName = repositoryType;
            string classFullName = string.Concat(repositoryType, ".CategoryRepository");

            var targetRepositoryType = Reflector.GetType(repositoryType, classFullName);
            container.Register(typeof(ICategoryRepository), targetRepositoryType);
        }
    }
}
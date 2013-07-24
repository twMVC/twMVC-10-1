using System;
using System.Web.Configuration;
using MultiProjects.Repository.Interface;

[assembly: WebActivator.PostApplicationStartMethod(typeof(MultiProjects.Web.MVC.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace MultiProjects.Web.MVC.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using MultiProjects.Domain.Utilities;
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
            // 1. Get Repository Assembly Name

            string repositoryType = WebConfigurationManager.AppSettings["RepositoryType"].ToString().Trim();

            //=====================================================================================
            // 2. Register Type for Repository

            string repositoryAssemblyName = repositoryType;
            string classFullName = string.Concat(repositoryType, ".CategoryRepository");

            Type targetRepositoryType = Reflector.GetType(repositoryType, classFullName);

            container.Register(typeof(ICategoryRepository), targetRepositoryType);
        }
    }
}
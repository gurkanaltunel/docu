using System;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DocumentServices.Abstractions;
using DocumentServices.Models;
using DocumentServices.Repository;
using ServiceStack.OrmLite;

namespace DocumentServices
{
    public class Bootstrapper : IDisposable
    {
        private readonly IWindsorContainer _container;

        public Bootstrapper(params IWindsorInstaller[] installers)
        {
            _container = new WindsorContainer().Install(installers);

            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static Bootstrapper StartApplication(params IWindsorInstaller[] installers)
        {
            return new Bootstrapper(installers);
        }

        public Bootstrapper InitializeDependencies()
        {
            _container.Register(Component.For<IDocumentService>().ImplementedBy<DefaultDocumentService>());
            _container.Register(Component.For<ISessionHelper>().ImplementedBy<DefaultSessionHelper>());
            //_container.Register(Component.For<IProfileRepository>().ImplementedBy<SqlProfileRepository>());
            return this;
        }

        public Bootstrapper InstallDatabase()
        {
            using (var connection = DbHelper.CreateConnection())
            {
                connection.CreateTable<User>();
                connection.CreateTable<Folder>();
                connection.CreateTable<File>();
                connection.CreateTable<FileVersion>();
                connection.CreateTable<Profile>();
                connection.CreateTable<Comment>();
                return this;
            }
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.MVC;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.BLL.Interfaces;
using OnlineGameStore.DAL.Interfaces;
using OnlineGameStore.DAL.Repositories;
using OnlineGameStore.DAL.DBContext;

namespace OnlineGameStore.Web.Ninject
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
             kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
             kernel.Bind<IGameStoreContext>().To<GameStoreContext>();
             kernel.Bind<ICommentRepository>().To<CommentRepository>();
             kernel.Bind<IGameRepository>().To<GameRepository>();
             kernel.Bind<IGameService>().To<GameService>();
             kernel.Bind<ICommentService>().To<CommentService>();
        }
    }
}
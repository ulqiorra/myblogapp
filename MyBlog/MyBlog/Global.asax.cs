using System;
using System.Web;
using MyBlog.Controllers;
using MyBlog.Core;
using MyBlog.Core.Objects;
using MyBlog.Providers;
using Ninject;
using Ninject.Web.Common;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace MyBlog
{
  public class MvcApplication : NinjectHttpApplication
  {
    protected override IKernel CreateKernel()
    {
      var kernel = new StandardKernel();

      kernel.Load(new RepositoryModule());
      kernel.Bind<IBlogRepository>().To<BlogRepository>();
      kernel.Bind<IAuthProvider>().To<AuthProvider>();

      return kernel;
    }

    protected override void OnApplicationStarted()
    {
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      ModelBinders.Binders.Add(typeof(Post), new PostModelBinder(Kernel));

      base.OnApplicationStarted();
    }

  }
}

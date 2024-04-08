using GS_Parfum.DAL.Interfaces;
using GS_Parfum.DAL.Repositories;
using GS_Parfum.Service.Implementations;
using GS_Parfum.Service.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GS_Parfum.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductRepository>().To<ProductRepository>();
            Bind<IProductService>().To<ProductService>();
        }
    }
}
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
        
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserService>().To<UserService>();

            Bind<ICartRepository>().To<CartRepository>();
            Bind<ICartService>().To<CartService>();

            Bind<IOrderRepository>().To<OrderRepository>();
            Bind<IOrderService>().To<OrderService>();

            Bind<IReviewRepository>().To<ReviewRepository>();
            Bind<IReviewService>().To<ReviewService>();
        }
    }
}
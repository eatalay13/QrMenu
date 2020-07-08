using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Data.UnitOfWork;
using Business.Managers;
using Business.Interfaces;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ProductBusiness>().As<IProductBusiness>();

            builder.RegisterType<CategoryBusiness>().As<ICategoryBusiness>();


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}

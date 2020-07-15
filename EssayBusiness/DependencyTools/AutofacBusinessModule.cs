using Autofac;
using EssayBusiness.Services.Abstract;
using EssayBusiness.Services.Concrete;
using EssayData.Services.Abstract;
using EssayData.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace EssayBusiness.DependencyTools
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builders
            builder.RegisterType<EssayService>().As<IEssayService>().InstancePerDependency();

            builder.RegisterType<EssayMongoService>().As<IEssayMongoService>()
                    .WithParameter("connectionString", "mongodb://localhost:27017/")
                    .WithParameter("databaseName", "essayMongoDb")
                    .WithParameter("collectionName", "essay")
                    .SingleInstance();


        }
    }
}

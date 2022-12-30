using Autofac;
using AutofacSample.Infrastructure.Services;
using AutofacSample.Infrastructure.Services.Interfaces;

namespace AutofacSample.Api.Autofac;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Registering a service
        // The default Instance scope is InstancePerDependency (Transient)
        // It creates a new instance per every request for the service.
        //builder.RegisterType<EmailService>().As<IEmailService>();
        // OR
        //builder.RegisterType<EmailService>().As<IEmailService>()
        //.InstancePerDependency();

        // Register a service With Scope life time.
        //builder.RegisterType<EmailService>()
        //    .As<IEmailService>()
        //    .InstancePerLifetimeScope();

        // Register a service With Singletone life time.
        //builder.RegisterType<EmailService>()
        //    .As<IEmailService>()
        //    .SingleInstance();

        // Register a service With Singletone life time.
        //builder.RegisterType<EmailService>()
        //    .As<IEmailService>()
        //    .InstancePerDependency();


        var infrastructureAssembly = System.Reflection.Assembly.Load("AutofacSample.Infrastructure");

        builder.RegisterAssemblyTypes(infrastructureAssembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces();
    }
}

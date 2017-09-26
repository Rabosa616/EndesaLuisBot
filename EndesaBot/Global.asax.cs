using Autofac;
using EndesaBot.Interfaces;
using EndesaBot.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace EndesaBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<GreetingService>()
                .Keyed<IGreeting>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.Update(Conversation.Container);
        }


    }
}

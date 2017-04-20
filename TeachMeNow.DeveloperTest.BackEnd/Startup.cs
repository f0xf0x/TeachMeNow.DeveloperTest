using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Autofac;
using Autofac.Integration.WebApi;

using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

using Owin;

using TeachMeNow.DeveloperTest.BackEnd.Models;

[assembly: OwinStartup(typeof(TeachMeNow.DeveloperTest.BackEnd.Startup))]

namespace TeachMeNow.DeveloperTest.BackEnd {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            var container = config.DependencyResolver;

            // the fastest way - to use service locator
            // the best to register dependency here
            BackEndDB db = (BackEndDB) container.GetService(typeof(BackEndDB));

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions() {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(db)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}

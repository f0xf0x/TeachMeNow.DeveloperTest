using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TeachMeNow.DeveloperTest.BackEnd.Models;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers.Tests {
    [TestClass]
    public abstract class ApiControllerTestBase {
        public HttpConfiguration HttpConfig { get; set; }
        public TestServer Server { get; set; }
        public HttpClient Client { get; set; }
        public ClaimsPrincipal User { get; set; }

        private void SetupClaimsPrincipal(User user) {
            var identity = new ClaimsIdentity(user.GetClaims());
            User = new ClaimsPrincipal(new List<ClaimsIdentity>(){identity});
        }

        protected void setUser(User user) {
            var config = new HttpConfiguration();
            // make sure that Web API sets up the same routes and configuration
            // as the app will in production
            WebApiConfig.Register(config);

            // more to come on this one
            config.MessageHandlers.Add(new UnitTestMessageHandler(this.User));
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            // In-memory Web API Server for unit testing
            Server = TestServer.Create(app => {
                app.UseWebApi(config);
            });

            Client = Server.HttpClient;
        }
    }

    public class UnitTestMessageHandler : DelegatingHandler {
        public UnitTestMessageHandler(ClaimsPrincipal user) {
            this.User = user;
        }

        public ClaimsPrincipal User { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(
                                HttpRequestMessage request,
                                CancellationToken cancellationToken) {
            // setup the Request's principal
            var ctx = request.GetRequestContext();
            ctx.Principal = this.User;
            request.SetRequestContext(ctx);

            // make all of our requests appear "local"
            request.Properties["MS_IsLocal"] = new Lazy<bool>(() => true);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
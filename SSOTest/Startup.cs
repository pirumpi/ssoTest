using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using SSOTest.SSOConfig;
using IdentityServer3.Core.Models;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(SSOTest.Startup))]

namespace SSOTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.Map("/identity", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Embedded IdentityServer",
                    SigningCertificate = LoadCertificate(),

                    Factory = new IdentityServerServiceFactory()
                                .UseInMemoryUsers(Users.Get())
                                .UseInMemoryClients(Clients.Get())
                                .UseInMemoryScopes(Scopes.Get())
                });
            });
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                @"C:\Apps\idsrv3test.pfx", "idsrv3test");
        }
    }
    
}

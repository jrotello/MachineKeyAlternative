using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(MachineKeyAlternative.Startup))]

namespace MachineKeyAlternative
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                LoginPath = new PathString("/home/login"),
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType
            });
        }
    }
}

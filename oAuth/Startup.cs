using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using oAuth.Core;
using oAuth.Model;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace oAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            //On lance l'application
            var config = new HttpConfiguration();

            //On configure les routes de l'api
            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
               );
            
            //on l'ajout à l'application
            appBuilder.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder appBuilder)
        {
            //Configure oAuth

            //Précise le context utilisé ( Ici la classe MyUser présente dans le dossier Model )
            appBuilder.CreatePerOwinContext(() => new UserManager<MyUser>(new UserStore<MyUser>(new MyDbContext())));

            //On configure la route pour l'oauth
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new MyAuthAuthorizationServerProvider()
            };

            //On ajoute la configuration oauth au serveur
            appBuilder.UseOAuthAuthorizationServer(oAuthServerOptions);
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}

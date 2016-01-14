using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using oAuth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oAuth.Core
{
    public class MyAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public async override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //Point d'entrée de d'une connexion oauth
            //Ici tout le monde peut demander un token
            context.Validated();
        }

        public async override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //On créer le usermanager
            var userManager = context.OwinContext.GetUserManager<UserManager<MyUser>> ();

            //On verifie le login et mot de passe
            var user = await userManager.FindAsync(context.UserName, context.Password);


            if(user == null)
            {
                //si user n'existe pas on envoie un message d'erreur
                context.Rejected();
                context.SetError("invalidate_grant", "Le login ou le mot de passe est incorrect");
                return;
            }

            //On créer un id unique
            var id = await userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
            //on créer le token complet
            var ticket = new AuthenticationTicket(id, null);
            //on le valide et il est renvoyer à "l'utilisateur"
            context.Validated(ticket);
        }
    }
}

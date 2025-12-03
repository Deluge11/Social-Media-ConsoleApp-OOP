using SocialApp.Interfaces;
using SocialApp.Model;
using SocialApp.Pages;
using SocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Scripts
{
    public class LoginAction : IAction
    {
        public string ActionName { get; } = "Login";
        public AuthenticationServices AuthenticationServices { get; }

        public LoginAction(AuthenticationServices authenticationServices)
        {
            AuthenticationServices = authenticationServices;
        }
        public void Action()
        {
            AuthenticationServices.Login();
        }

    }
}

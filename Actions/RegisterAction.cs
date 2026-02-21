using SocialApp.Interfaces;
using SocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Scripts
{
    public class RegisterAction : IAction
    {
        public string ActionName { get; } = "Register";
        public AuthenticationServices AuthenticationServices { get; }

        public RegisterAction(AuthenticationServices authenticationServices)
        {
            AuthenticationServices = authenticationServices;
        }
        public void Action()
        {
            AuthenticationServices.Register();
        }

    }
}

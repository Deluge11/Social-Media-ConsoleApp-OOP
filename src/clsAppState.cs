using SocialApp.Model;
using System.Net.NetworkInformation;

namespace SocialApp
{
    public class clsAppState
    {
        public clsUser User { get; set; }
        public bool IsGuest { get; set; }

        public bool IsAuthenticated()
        {
            return User != null;
        }

        public void Clear()
        {
            User = null!;
            IsGuest = false;
        }
    }
}

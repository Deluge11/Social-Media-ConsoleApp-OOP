using SocialApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp
{
    public class AppState
    {
        public User User { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}

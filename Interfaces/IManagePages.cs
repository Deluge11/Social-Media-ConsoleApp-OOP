using SocialApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Interfaces
{
    public interface IManagePages
    {
        List<AbPage> Pages { get; }
        void AddPage(AbPage page);
    }
}

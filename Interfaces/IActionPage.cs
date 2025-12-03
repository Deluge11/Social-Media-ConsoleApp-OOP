using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Interfaces
{
    public interface IActionPage
    {
        List<IAction> Actions { get; }
        void AddAction(IAction action);
    }
}

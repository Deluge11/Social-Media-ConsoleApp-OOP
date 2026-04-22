using SocialApp.Enums;

namespace SocialApp.Interfaces.Page
{
    public interface IAction
    {
        enPermission ActionPermission { get; }
        string ActionName { get; }
        void Execute();
    }
}


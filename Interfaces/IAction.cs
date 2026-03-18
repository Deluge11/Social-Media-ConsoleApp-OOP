using SocialApp.Enums;

namespace SocialApp.Interfaces
{
    public interface IAction
    {
        enPermission ActionPermission { get; }
        string ActionName { get; }
        void Execute();
    }
}


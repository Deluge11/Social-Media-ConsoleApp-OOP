
namespace SocialApp.Interfaces
{
    public interface IAction
    {
        string ActionName { get; }
        void Execute();
    }
}


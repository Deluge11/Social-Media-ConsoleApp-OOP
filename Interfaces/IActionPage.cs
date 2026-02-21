

namespace SocialApp.Interfaces
{
    public interface IActionPage
    {
        List<IAction> Actions { get; }
        void AddAction(IAction action);
    }
}

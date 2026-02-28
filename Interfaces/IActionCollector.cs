

namespace SocialApp.Interfaces
{
    public interface IActionCollector
    {
        List<IAction> Actions { get; }
        void AddAction(IAction action);
    }
}

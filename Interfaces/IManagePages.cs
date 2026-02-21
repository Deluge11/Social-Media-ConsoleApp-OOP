using SocialApp.Abstractions;


namespace SocialApp.Interfaces
{
    public interface IManagePages
    {
        List<AbPage> Pages { get; }
        void AddPage(AbPage page);
    }
}

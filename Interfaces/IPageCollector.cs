using SocialApp.Abstractions;


namespace SocialApp.Interfaces
{
    public interface IPageCollector
    {
        List<AbPage> Pages { get; }
        void AddPage(AbPage page);
    }
}

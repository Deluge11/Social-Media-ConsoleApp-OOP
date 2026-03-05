using SocialApp.Abstractions;


namespace SocialApp.Interfaces
{
    public interface IPageCollector
    {
        List<absPage> Pages { get; }
        void AddSubPage(absPage page);
    }
}

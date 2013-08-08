namespace LifeIdea.LazyCure.Core.Localization
{
    public interface ILanguageSwitcher
    {
        void ChangeLanguage(string languageCode);
        string LastApplied { get; }
    }
}

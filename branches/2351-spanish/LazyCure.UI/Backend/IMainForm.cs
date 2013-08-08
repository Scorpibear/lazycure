namespace LifeIdea.LazyCure.UI.Backend
{
    public interface IMainForm
    {
        bool PostToTwitterEnabled { set;}
        void RegisterHotKeys();
        void ViewsVisibilityChanged();
    }
}
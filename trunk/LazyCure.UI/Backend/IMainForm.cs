namespace LifeIdea.LazyCure.UI
{
    public interface IMainForm
    {
        bool PostToTwitterEnabled { set;}
        void RegisterHotKeys();
        void ViewsVisibilityChanged();
    }
}
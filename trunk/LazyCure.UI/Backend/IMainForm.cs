namespace LifeIdea.LazyCure.UI
{
    public interface IMainForm
    {
        bool PostToTwitterEnabled { set;}
        void RegisterHotKey();
        void ViewsVisibilityChanged();
    }
}
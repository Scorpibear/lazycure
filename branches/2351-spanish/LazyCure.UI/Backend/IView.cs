namespace LifeIdea.LazyCure.UI.Backend
{
    public interface IView
    {
        void Show();

        bool Visible { get; set; }
    }
}

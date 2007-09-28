namespace LifeIdea.LazyCure.UI.Interfaces
{
    interface ITimeLogView
    {
        void CancelEdit();
        void Show();
        bool Visible { get; set; }
    }
}

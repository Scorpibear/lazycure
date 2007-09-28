namespace LifeIdea.LazyCure.UI
{
    interface ITimeLogView
    {
        void CancelEdit();
        void Show();
        bool Visible { get; set; }
    }
}

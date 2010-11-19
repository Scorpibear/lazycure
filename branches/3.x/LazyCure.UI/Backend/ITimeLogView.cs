namespace LifeIdea.LazyCure.UI
{
    interface ITimeLogView
    {
        bool Visible { get; set; }

        void CancelEdit();

        void Show();

        void Update();
    }
}

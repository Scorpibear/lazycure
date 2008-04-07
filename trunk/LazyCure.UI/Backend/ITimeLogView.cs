namespace LifeIdea.LazyCure.UI
{
    interface ITimeLogView
    {
        object Data { set; }

        bool Visible { get; set; }

        void CancelEdit();

        void Show();
    }
}
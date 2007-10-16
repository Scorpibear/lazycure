namespace LifeIdea.LazyCure.Core.IO
{
    public interface IWriter
    {
        void WriteLine(string s);
        void Close();
    }
}
namespace Library.Common.Utils
{
    /// <summary>
    /// asd asds
    /// </summary>
    public class FID
    {
        public int CurrentIterator { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public int WhiceIterator { get; set; }
        public char Begin { get; set; }
        public char End { get; set; }
        public bool IsReaded
        {
            get { return CurrentIterator >= WhiceIterator; }
        }
        public void Ok()
        {
            CurrentIterator++;
        }
    }
}

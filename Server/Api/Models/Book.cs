namespace Api.Models
{
    public class Book : Item
    {
        #region Properties
        public int? Pages { get; set; }
        #endregion

        #region Constructors

        public Book()
        {
        }

        public Book(string title, string summary) : this(title, summary, null, null)
        {
        }

        public Book(string title, string summary, string image = null, string url = null) : this(title, summary, image, url, -1)
        {
        }
        
        public Book(string title, string summary, string image = null, string url = null, int? pages = -1) : base(title, summary, image, url)
        {
            if (pages is <= 0)
            {
                Pages = null;
            } 
            Pages = pages;
        }
        #endregion
    }
}
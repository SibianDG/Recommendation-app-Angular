namespace Api.Models
{
    public class Movie : Item
    {
        #region Properties
        public int? Duration { get; set; }
        #endregion

        #region Constructors

        public Movie()
        {
        }

        public Movie(string title, string summary) : this(title, summary, null, null)
        {
        }

        public Movie(string title, string summary, string image = null, string url = null) : this(title, summary, image, url, -1)
        {
        }
        
        public Movie(string title, string summary, string image = null, string url = null, int? duration = -1) : base(title, summary, image, url)
        {
            if (duration is <= 0)
            {
                Duration = null;
            }
            Duration = duration;
        }
        #endregion
  
    }
}
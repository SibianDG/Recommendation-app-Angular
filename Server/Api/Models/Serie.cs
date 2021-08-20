namespace Api.Models
{
    public class Serie : Item
    {
        #region Properties
        public int? NumberOfEpisodes { get; set; }
        #endregion
        
        #region Constructors

        public Serie()
        {
        }

        public Serie(string title, string summary) : this(title, summary, null, null)
        {
        }

        public Serie(string title, string summary, string image = null, string url = null) : this(title, summary, image, url, -1)
        {
        }
        
        public Serie(string title, string summary, string image = null, string url = null, int? numberOfEpisodes = -1) : base(title, summary, image, url)
        {
            if (numberOfEpisodes is <= 0)
            {
                numberOfEpisodes = null;
            }

            NumberOfEpisodes = numberOfEpisodes;
        }
        #endregion

    }
}
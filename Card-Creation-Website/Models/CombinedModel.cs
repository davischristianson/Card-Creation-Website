namespace Card_Creation_Website.Models
{
    public class CombinedModel
    {
        /// <summary>
        /// This model is the card model that will contain
        /// the info for the respective model submitted on 
        /// the Card Create form
        /// </summary>
        public Card CardModel { get; set; }

        /// <summary>
        /// This model is the image model that will contain
        /// the image submitted on the Card Create Page
        /// </summary>
        public ImageModel ImageModel { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Card_Creation_Website.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string CardName { get; set; }

        [Required]
        public string CardDescription { get; set; }

        /// <summary>
        /// Link found trying to store image in the Card class
        /// https://stackoverflow.com/questions/33012853/how-to-add-image-property-in-my-model
        /// </summary>
        [Required]
        public string CardImage { get; set; }

        public string CardType { get; set; }

        public string CardEffect { get; set; }

        public string SpellType { get; set; }

        public string Rarity { get; set; }

        public string ElementalType { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Health { get; set; }

        public int Cost { get; set; }

        public string SerialNumber { get; set; }

    }
}

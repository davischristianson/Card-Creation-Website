using System.ComponentModel.DataAnnotations;

namespace Card_Creation_Website.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        public int UserId { get; set; }

        [Required]
        [Display(Name = "Card Name")]
        public string CardName { get; set; }

        [Required]
        [Display(Name = "Card Description")]
        public string CardDescription { get; set; }

        /// <summary>
        /// Link found trying to store image in the Card class
        /// https://stackoverflow.com/questions/33012853/how-to-add-image-property-in-my-model
        /// </summary>
        [Required]
        [Display(Name = "Card Image")]
        public string CardImage { get; set; }

        [Display(Name = "Card Type")]
        public string CardType { get; set; }

        [Display(Name = "Card Effect")]
        public string CardEffect { get; set; }

        [Display(Name = "Spell Type")]
        public string SpellType { get; set; }

        public string Rarity { get; set; }

        [Display(Name = "Element Type")]
        public string ElementalType { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Health { get; set; }

        public int Cost { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

    }
}

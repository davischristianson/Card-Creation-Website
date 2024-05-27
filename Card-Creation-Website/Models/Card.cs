using System.ComponentModel.DataAnnotations;

namespace Card_Creation_Website.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        /// <summary>
        /// Name of the Card
        /// </summary>
        [Required]
        [Display(Name = "Card Name")]
        public string CardName { get; set; }

        /// <summary>
        /// Description of the created card.
        /// History, details, info, etc.
        /// </summary>
        [Required]
        [Display(Name = "Card Description")]
        public string CardDescription { get; set; }

        /// <summary>
        /// Link found trying to store image in the Card class
        /// https://stackoverflow.com/questions/33012853/how-to-add-image-property-in-my-model
        /// {{{{{{{   IMPLEMENT THIS LAST!!!!!    }}}}}}}
        /// </summary>
        [Required]
        [Display(Name = "Card Image")]
        public string CardImage { get; set; }

        /// <summary>
        /// Whether it is a spell, unit, trap, etc.
        /// </summary>
        [Display(Name = "Card Type")]
        public string CardType { get; set; }

        /// <summary>
        /// This determines the effect it has but only if conditions are met.
        /// Such as it has a paralysis effect if it is a lightning trap card.
        /// The key being the type of card it is and what it specifically does will
        /// indicate that the card has an effect
        /// </summary>
        [Display(Name = "Card Effect")]
        public string CardEffect { get; set; }

        /// <summary>
        /// Spell Type is similar to Card Effect with the exception that
        /// the card needs to be a spell in order for it to be a spell type.
        /// Spell types could include offensive, support, healing, utility, etc.
        /// </summary>
        [Display(Name = "Spell Type")]
        public string SpellType { get; set; }

        /// <summary>
        /// How rare a card is.
        /// For instance a card depicting a god could be a "divine" or something
        /// similar as a rarity whereas a simple animal card could be considered common.
        /// Ex. of rarity are (in ascending order, left to right):
        /// Common, Uncommon, Rare, Epic, Legendary, Mythical, Divine, Primordial, etc.
        /// </summary>
        public string Rarity { get; set; }

        /// <summary>
        /// Element type is the elemental attunement a card has regardless of 
        /// Card Type or Spell Type, should be independent of any other stat.
        /// Ex. of elemental types are:
        /// Lightning, Fire, Water, Air, Earth, Light, Dark, Void, Space, Time, Neutral etc.
        /// </summary>
        [Display(Name = "Element Type")]
        public string ElementalType { get; set; }

        /// <summary>
        /// Referring to how much attack a card has numerically.
        /// This stat is used to numerically quantify a card's offensive power.
        /// Some cards could not use this stat.
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// Referring to how much defense a card has numerically.
        /// This stat is used to numerically quantify a card's defensive power.
        /// Some cards could not use this stat.
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// Referring to how much health a card has numerically.
        /// This stat is used to numerically quantify a card's total health.
        /// Some cards could not use this stat.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// The cost of using/playing this card during a turn.
        /// Ex. Playing a card costs energy on a players turn. A card costing
        /// more resources then a player has could result in not being able to
        /// play that card.
        /// Ex2. 10 energy to play card, player has 9 energy, cannot play card
        /// that round.
        /// Cards that don't have a cost would be marked as 0 cost. 
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// The special number dictating the uniqueness of the card.
        /// Can include extra information detailing what the card belongs to.
        /// Ex. WAT-SP-1-1 = Water-Spell-1st edition-1st print
        /// Ex2. GUS194315 = GrandUnitSet19thEdition4315thPrint
        /// </summary>
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// The relation to the account that made the card.
        /// This is a foreign key.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// This is the required reference navigation to the Account class.
        /// The = null means it must be related to an account, meaning a card 
        /// has to be connected to an account.
        /// </summary>
        public Account Account { get; set; } = null!;
    }
}
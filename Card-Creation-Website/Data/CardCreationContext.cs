using Card_Creation_Website.Models;
using Microsoft.EntityFrameworkCore;

namespace Card_Creation_Website.Data
{
    public class CardCreationContext : DbContext
    {
        public CardCreationContext(DbContextOptions<CardCreationContext> options) : base(options) 
        { 
        
        }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Account> Accounts { get; set; }
    }
}

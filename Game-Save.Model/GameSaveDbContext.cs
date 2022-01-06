using Microsoft.EntityFrameworkCore;

namespace Game_Save.Model
{
    public class GameSaveDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameSlot> GameSlots { get; set; }
        public DbSet<GameSave> GameSaves { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\GameSave.db;Version=3;");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Game_Save.Model
{
    public class GameSaveDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameSlot> GameSlots { get; set; }
        public DbSet<GameSave> GameSaves { get; set; }
        
        public GameSaveDbContext(DbContextOptions<GameSaveDbContext> options)
            :base(options){ }
    }
}
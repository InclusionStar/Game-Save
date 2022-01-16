using System.Collections.Generic;
using System.IO;

namespace Game_Save.Model
{
    public class GameSlot
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public List<GameSave>? GameSaves { get; set; }
    }
}
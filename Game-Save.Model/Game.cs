using System.Collections.Generic;

namespace Game_Save.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public List<GameSlot>? GameSlots { get; set; }
    }
}
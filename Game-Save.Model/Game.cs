using System.Collections.Generic;

namespace Game_Save.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public int MostPercentage { get; set; }
        public int HasOneSlot { get; set; }

        public List<GameSlot> GameSlots { get; set; }
    }
}
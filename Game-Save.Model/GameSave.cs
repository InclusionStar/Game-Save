namespace Game_Save.Model
{
    public class GameSave
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Notes { get; set; }
        public int Percentage { get; set; }
        public int MarkedByStar { get; set; }
        
        public int GameSlotsId { get; set; }
        public GameSlot GameSlot { get; set; }
    }
}
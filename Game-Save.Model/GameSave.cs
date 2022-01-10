namespace Game_Save.Model
{
    public class GameSave
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int GameSlotId { get; set; }
        public GameSlot GameSlot { get; set; }
    }
}
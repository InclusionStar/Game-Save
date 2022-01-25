using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped] public FileSystemWatcher? SlotWatcher { get; set; }
        
        public void StartListening()
        {
            SlotWatcher = new FileSystemWatcher();
            SlotWatcher.Changed += OnChange;
            SlotWatcher.NotifyFilter = NotifyFilters.LastWrite;
            SlotWatcher.Path = System.IO.Path.GetDirectoryName(Path); 
            SlotWatcher.Filter = System.IO.Path.GetFileName(Path);
            SlotWatcher.EnableRaisingEvents = true;
        }

        private void OnChange(object sender, FileSystemEventArgs e)
        {
            var fileName =  System.IO.Path.GetFileName(Path);
            if (Path != null) 
                File.Copy(Path, $"./Storage/{Name}/{fileName}");
        }
    }
}
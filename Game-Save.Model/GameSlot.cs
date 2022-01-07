using System.Collections.Generic;
using System.IO;

namespace Game_Save.Model
{
    public class GameSlot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Notes { get; set; }
        public int MostPercentage { get; set; }
        public int IsCompleted { get; set; }
        public int IsAutosave { get; set; }
        public int LastSavesNum { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public List<GameSave> GameSaves { get; set; }

        
        public void StartListening()
        {
            var watcher = new FileSystemWatcher();
            watcher.Changed += OnChange;
        }

        private void OnChange(object sender, FileSystemEventArgs e)
            => File.Copy(Path, @"\Storage\");
    }
}
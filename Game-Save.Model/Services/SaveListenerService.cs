using System.IO;
using System.Windows.Controls.Ribbon;

namespace Game_Save.Model.Services;

public class SaveListenerService
{
    private GameSave gameSave { get; set; }

    public SaveListenerService(GameSave gameSave)
    {
        this.gameSave = gameSave;
    }

    public FileSystemWatcher StartListening()
    {
        var watcher = new FileSystemWatcher();
        watcher.Changed += OnChange;
        watcher.NotifyFilter = NotifyFilters.LastWrite;
        watcher.Path = Path.GetDirectoryName(gameSave.Path); 
        watcher.Filter = Path.GetFileName(gameSave.Path);
        watcher.EnableRaisingEvents = true;

        return watcher;
    }

    private void OnChange(object sender, FileSystemEventArgs e)
    {
        var fileName =  Path.GetFileName(gameSave.Path);
        if (gameSave.Path != null) 
            File.Copy(gameSave.Path, $"./Storage/{gameSave.GameSlot.Name}/{fileName}");
    }
}
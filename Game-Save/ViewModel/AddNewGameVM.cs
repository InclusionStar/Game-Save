using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Game_Save.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace Game_Save.ViewModel
{
    public class AddNewGameVM : INotifyPropertyChanged
    {
        private readonly MainWindowVM parentVM;
        private string gamePath;
        private GameSaveDbContext db;
        
        #region Bindings
        public string GameName { get; set; }
        
        public string GamePath
        {
            get => gamePath;
            set
            {
                gamePath = value;
                OnPropertyChanged();
            }
        }
        #endregion
        
        #region Commands
        public RelayCommand OpenDialogWindow
            => new RelayCommand(obj =>
            {
                OpenFileDialog openFileDialog = new();
                openFileDialog.Filter = "All files(*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                    GamePath = openFileDialog.FileName;
            });

        public RelayCommand AddGame
            => new RelayCommand(obj =>
            {
                var window = obj as Window;
                var gameSave = AddNewGame();
                var fileName = Path.GetFileName(gameSave.Path);
                
                parentVM.GameSaves.Add(gameSave);
                
                if (File.Exists(GamePath))
                {
                    if (!Directory.Exists($"./Storage/"))
                        Directory.CreateDirectory($"./Storage/");
                    if (!Directory.Exists($"./Storage/{GameName}/"))
                        Directory.CreateDirectory($"./Storage/{GameName}/");
                    
                    File.Copy(GamePath, $"./Storage/{GameName}/{fileName}");
                    // gameSave.GameSlot.StartListening();
                }

                window.Close();
            });
        #endregion

        public AddNewGameVM(MainWindowVM parentVM)
        {
            this.parentVM = parentVM;
            db = new GameSaveDbContext();
            db.Games.Load();
            db.GameSlots.Load();
            db.GameSaves.Load();
        }

        private GameSave AddNewGame()
        {
            var gameSave = new GameSave
            {
                GameSlot = new GameSlot
                {
                    Path = GamePath, Name = GameName,
                    Game = new Game { Title = GameName }
                },
                Path = GamePath
            };
            db.GameSaves.Add(gameSave);
            db.SaveChanges();

            return gameSave;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
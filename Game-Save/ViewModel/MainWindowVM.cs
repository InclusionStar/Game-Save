using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Game_Save.View;
using Game_Save.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;


namespace Game_Save.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private GameSaveDbContext db;

        public ObservableCollection<Game> Games { get; set; }
        public ObservableCollection<GameSlot> GameSlots { get; set; }
        public ObservableCollection<GameSave> GameSaves { get; set; }
        
        public RelayCommand OpenAddGameWnd 
            => new RelayCommand( _ =>
            {
                AddNewGame addGame = new AddNewGame(this);
                addGame.Owner = Application.Current.MainWindow;
                addGame.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addGame.ShowDialog();
            });

        public RelayCommand OpenPathGameWnd
            => new RelayCommand(obj =>
            {
                var path = obj as string;
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer", 
                    Arguments = $"/n, /select, {path}"
                });
            });

        public MainWindowVM()
        {
            db = new GameSaveDbContext();
            db.Games.Load();
            db.GameSlots.Load();
            db.GameSaves.Load();
            Games = db.Games.Local.ToObservableCollection();
            GameSlots = db.GameSlots.Local.ToObservableCollection();
            GameSaves = db.GameSaves.Local.ToObservableCollection();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Game_Save.View;
using Game_Save.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;


namespace Game_Save.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private GameSaveDbContext db;
        public ObservableCollection<Game> games;
        private ObservableCollection<GameSlot> gameSlots;
        public ObservableCollection<GameSave> gameSaves;
        
        // public ObservableCollection<Game> AllGames
        // {
        //     get
        //     {
        //         using (GameSaveDbContext db = new GameSaveDbContext())
        //         {
        //             try
        //             {
        //                 var E = new ObservableCollection<Game>();
        //                 foreach (var a in db.Games.ToList())
        //                 {
        //                     E.Add(a);
        //                 }
        //                 return E;
        //             }
        //             catch
        //             {
        //                 return new ObservableCollection<Game>();
        //             }
        //         }
        //     }
        //     private set
        //     {
        //         OnPropertyChanged("AllGames");
        //     }
        // }

        public MainWindowVM()
        {
            db = new GameSaveDbContext();
            db.Games.Load();
            db.GameSlots.Load();
            db.GameSaves.Load();
            games = db.Games.Local.ToObservableCollection();
            gameSlots = db.GameSlots.Local.ToObservableCollection();
            gameSaves = db.GameSaves.Local.ToObservableCollection();
        }

        private RelayCommand? openAddGameWnd;
        public RelayCommand OpenAddGameWnd =>
            openAddGameWnd ?? new RelayCommand( _ =>
            {
                AddNewGame addGame = new AddNewGame(this);
                addGame.Owner = Application.Current.MainWindow;
                addGame.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addGame.ShowDialog();
            });

        // private RelayCommand openPathGameWnd;
        // public RelayCommand OpenPathGameWnd
        // {
        //     get
        //     {
        //         return openPathGameWnd ?? new RelayCommand(obj =>
        //         {
        //             OpenPathGameWindowMethod();
        //         }
        //         );
        //     }
        // }
        // private static void OpenPathGameWindowMethod()
        // {
        //     LocationOfGame pathGame = new LocationOfGame();
        //     pathGame.Owner = Application.Current.MainWindow;
        //     pathGame.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        //     pathGame.ShowDialog();
        // }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

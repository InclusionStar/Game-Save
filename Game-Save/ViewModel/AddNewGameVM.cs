using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using Game_Save.Model;
using Game_Save.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace Game_Save.ViewModel
{
    public class AddNewGameVM
    {
        private GameSaveDbContext db;
        private IEnumerable<Game> games;
        private IEnumerable<GameSlot> gameSlots;
        private IEnumerable<GameSave> gameSaves;

        private RelayCommand addGame;

        public string GameName { get; set; }
        public string GamePath { get; set; }


        private RelayCommand openDialogWindow;
        public RelayCommand OpenDialogWindow
            => openDialogWindow = new RelayCommand(obj =>
            {
                OpenFileDialog openFileDialog = new();
                openFileDialog.Filter = "All files(*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                    GamePath = openFileDialog.FileName;
            });

        public RelayCommand AddGame
        {
            get => addGame = new RelayCommand(obj =>
            {
                var window = obj as Window;
                var gameSave = new GameSave();
                gameSave.GameSlot = new GameSlot { Path = GamePath,Name = GameName, };
                gameSave.GameSlot.Game = new Game { Title = GameName};
                gameSave.Path = GamePath;
                db.GameSaves.Add(gameSave);
                db.SaveChanges();
                UpdateAllDepartmentsView();
                if (File.Exists(GamePath))
                {
                    //File.Copy(GamePath, $"./Storage/{GameName}/");
                    //gameSave.GameSlot.StartListening();
                }

                window.Close();
            });
        }

        public AddNewGameVM()
        {
            db = new GameSaveDbContext();
            db.Games.Load();
            db.GameSlots.Load();
            db.GameSaves.Load();
            games = db.Games.Local.ToBindingList();
            gameSlots = db.GameSlots.Local.ToBindingList();
            gameSaves = db.GameSaves.Local.ToBindingList();
        }

        private void UpdateAllDepartmentsView()
        {
            var AllGames = db.Games.ToList();
            MainWindow.AllGamesView.ItemsSource = null;
            MainWindow.AllGamesView.Items.Clear();
            MainWindow.AllGamesView.ItemsSource = AllGames;
            MainWindow.AllGamesView.Items.Refresh();
        }
    }
}
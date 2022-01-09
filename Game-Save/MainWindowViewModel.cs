using System.Collections.Generic;
using System.Linq;
using Game_Save.Model;
using Microsoft.EntityFrameworkCore;

namespace Game_Save
{
    public class MainWindowViewModel
    {
        private GameSaveDbContext db;
        private RelayCommand addGame;
        private RelayCommand addGameSlot;
        private RelayCommand deleteGame;
        private RelayCommand deleteGameSlot;
        private RelayCommand startTrackingSave;

        private IEnumerable<Game> games;
        private IEnumerable<GameSlot> gameSlots;
        private IEnumerable<GameSave> gameSaves;

        #region Bindings
        
        public Game GameToAdd { get; set; }
        
        #endregion
        
        #region Commands
        
        public RelayCommand AddGame
        {
            get => addGame = new RelayCommand(obj =>
            {
                var game = obj as Game;
                db.Games.Add(game);
                db.SaveChanges();
            });
        }
        public static List<Game> GetAllGames()
        {
            using (GameSaveDbContext db = new GameSaveDbContext())
            {
                try
                {
                    var result = db.Games.ToList();
                    return result;
                }
                catch
                {
                    return new List<Game>();
                }
            }
        }

        public RelayCommand AddGameSlot
        {
            get => addGameSlot = new RelayCommand(obj =>
            {
                var gameSlot = obj as GameSlot;
                db.GameSlots.Add(gameSlot);
                db.SaveChanges();
            });
        }

        public RelayCommand DeleteGame
        {
            get => deleteGame = new RelayCommand(obj =>
            {
                var game = obj as Game;
                db.Games.Remove(game);
                db.SaveChanges();
            });
        }

        public RelayCommand DeleteGameSlot
        {
            get => deleteGameSlot = new RelayCommand(obj =>
            {
                var gameSlot = obj as GameSlot;
                db.GameSlots.Remove(gameSlot);
                db.SaveChanges();
            });
        }

        public RelayCommand StartTrackingSave
        {
            get => startTrackingSave = new RelayCommand(obj =>
            {
                var gameSlot = obj as GameSlot;
                gameSlot.StartListening();
                db.SaveChanges();
            });
        }
        
        #endregion

        public MainWindowViewModel()
        {
            db = new GameSaveDbContext();
            db.Games.Load();
            db.GameSlots.Load();
            db.GameSaves.Load();
            games = db.Games.Local.ToBindingList();
            gameSlots = db.GameSlots.Local.ToBindingList();
            gameSaves = db.GameSaves.Local.ToBindingList();
        }
    }
}
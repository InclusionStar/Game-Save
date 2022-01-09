using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Game_Save.Model;

namespace Game_Save.View
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class AddNewGame : Window
    {
        public AddNewGame()
        {
            InitializeComponent();
            db = new GameSaveDbContext();
        }

        GameSaveDbContext db;

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            string name = nameGame.Text.Trim();
            string wayGame = wayToGame.Text.Trim();
            Game newGame = new Game();
            newGame.Title = name;
            db.Games.Add(newGame);
            db.SaveChanges();
        }
    }
}

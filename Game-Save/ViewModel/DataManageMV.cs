using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Game_Save.View;
using Game_Save.Model;
using System.ComponentModel;


namespace Game_Save.ViewModel
{
    public class DataManageMV : INotifyPropertyChanged
    {
        private List<Game> allGames = MainWindowViewModel.GetAllGames();
        public List<Game> AllGames
        {
            get
            {
                return allGames;
            }
            private set
            {
                allGames = value;
                NotifyPropertyChanged("AllGames");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;


        private readonly RelayCommand openAddGameWnd;
        public RelayCommand OpenAddGameWnd
        {
            get {
                return openAddGameWnd ?? new RelayCommand( obj =>
                    {
                        OpenAddGameWindowMethod();
                    }
                    );
            }
        }


        private static void OpenAddGameWindowMethod()
        {
            AddNewGame addGame = new AddNewGame();
            addGame.Owner = Application.Current.MainWindow;
            addGame.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addGame.ShowDialog();
        }


        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

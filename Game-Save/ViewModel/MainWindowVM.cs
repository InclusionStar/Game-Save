using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Game_Save.View;
using Game_Save.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace Game_Save.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public ObservableCollection<Game> AllGames
        {
            get
            {
                using (GameSaveDbContext db = new GameSaveDbContext())
                {
                    try
                    {
                        var E = new ObservableCollection<Game>();
                        foreach (var a in db.Games.ToList())
                        {
                            E.Add(a);
                        }
                        return E;
                    }
                    catch
                    {
                        return new ObservableCollection<Game>();
                    }
                }
            }
            private set
            {
                NotifyPropertyChanged("AllGames");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;


        private RelayCommand openAddGameWnd;
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

        private RelayCommand openPathGameWnd;
        public RelayCommand OpenPathGameWnd
        {
            get
            {
                return openPathGameWnd ?? new RelayCommand(obj =>
                {
                    OpenPathGameWindowMethod();
                }
                );
            }
        }
        private static void OpenPathGameWindowMethod()
        {
            LocationOfGame pathGame = new LocationOfGame();
            pathGame.Owner = Application.Current.MainWindow;
            pathGame.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            pathGame.ShowDialog();
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

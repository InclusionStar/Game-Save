using System.Windows;
using Game_Save.Model;
using Game_Save.ViewModel;

namespace Game_Save.View
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class AddNewGame : Window
    {
        public AddNewGame(MainWindowVM parentVM)
        {
            InitializeComponent();
            DataContext = new AddNewGameVM(parentVM);
        }
    }
}
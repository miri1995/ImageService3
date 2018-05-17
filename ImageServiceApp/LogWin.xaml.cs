using ImageServiceApp.ViewModel;
using System.Windows.Controls;



namespace ImageServiceApp
{
    /// <summary>
    /// Interaction logic for LogWin.xaml
    /// </summary>
    public partial class LogWin : UserControl
    {
     

        public LogWin()
        {
            InitializeComponent();
            this.DataContext = new LogViewModel();
        }
    }
}
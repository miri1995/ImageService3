using ImageServiceApp.ViewModel;
using System.Windows.Controls;



namespace ImageServiceApp
{
    /// <summary>
    /// Interaction logic for LogWin.xaml
    /// </summary>
    public partial class LogWin : UserControl
    {
        private LogViewModel LogsViewModel;

        public LogWin()
        {
            InitializeComponent();
            LogsViewModel = new LogViewModel();
            this.DataContext = LogsViewModel;
        }
    }
}
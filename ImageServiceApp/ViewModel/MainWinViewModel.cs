using ImageServiceApp.Model;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace ImageServiceApp.ViewModel
{
    public class MainWinViewModel: IMainWinViewModel
    {
        private IMainWinModel m_mainWindowModel;
        public bool VM_IsConnected
        {
            get
            {
                return this.m_mainWindowModel.IsConnected;
            }
        }

        public ICommand CloseCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// MainWindowVM constructor.
        /// </summary>
        public MainWinViewModel()
        {
            this.m_mainWindowModel = new MainWinModel();
            this.m_mainWindowModel.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("View Model" + e.PropertyName);
            };

            this.CloseCommand = new DelegateCommand<object>(this.Close, this.CanClose);
        }
        /// <summary>
        /// OnClose function. 
        /// defines what happens before the window is closed.
        /// </summary>
        /// <param name="obj"></param>
        private void Close(object obj)
        {
            this.m_mainWindowModel.GuiClient.Disconnected();
        }
        /// <summary>
        /// CanClose function.
        /// defines if user can close the window.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanClose(object obj)
        {
            return true;
        }

        /// <summary>
        /// NotifyPropertyChanged function.
        /// invokes PropertyChanged event about change of property.
        /// </summary>
        /// <param name="propName">the changed property</param>
        private void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(propName);
            this.PropertyChanged?.Invoke(this, propertyChangedEventArgs);
        }

    }
}

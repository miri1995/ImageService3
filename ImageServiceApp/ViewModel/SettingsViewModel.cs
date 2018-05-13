using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ImageServiceApp.Model;
using Prism.Commands;



namespace ImageServiceApp.ViewModel
{
    class SettingsViewModel:ISettingsViewModel
    {
        private SettingsModel SettingsModel;

        public string OutputDirectory
        {
            get { return this.SettingsModel.OutputDirectory; }
            set
            {
                this.SettingsModel.OutputDirectory = value;
            }
        }

        public string SourceName
        {
            get { return this.SettingsModel.SourceName; }
            set
            {
                this.SettingsModel.SourceName = value;
            }
        }

        public string LogName
        {
            get { return this.SettingsModel.LogName; }
            set
            {
                this.SettingsModel.LogName = value;
            }
        }

        public string ThumbSize
        {
            get { return this.SettingsModel.ThumbSize; }
            set
            {
                this.SettingsModel.ThumbSize = value;
            }
        }


        public string ChosenHandler
        {
            get { return this.SettingsModel.ChosenHandler; }
            set
            {
                this.SettingsModel.ChosenHandler = value;
                var command = this.RemoveCommand as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<string> Directories
        {
            get { return this.SettingsModel.Directories; }
            set
            {
                this.SettingsModel.Directories = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
            SettingsModel = new SettingsModel();
            SettingsModel.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e) {
                   NotifyPropertyChanged("View Model" + e.PropertyName);
               };
            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
        }

        public ICommand RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            this.SettingsModel.sendToServer();
        }

        private bool CanRemove(object obj)
        {
            if (string.IsNullOrEmpty(this.SettingsModel.ChosenHandler))
            {
                return false;
            }
            return true;
        }


        protected void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
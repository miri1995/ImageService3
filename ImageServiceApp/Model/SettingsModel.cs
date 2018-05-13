using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ImageServiceApp.Event;
using ImageServiceApp.Enums;
using ImageServiceApp.Model;
using ImageServiceApp.Communication;

namespace ImageServiceApp.Models
{
    class SettingsModel : INotifyPropertyChanged,ISettingsModel
    {
        // an event that raises when a property is being changed
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<string> m_Directories;
        private string m_OutputDirectory;
        private string m_SourceName;
        private string m_LogName;
        private string m_ThumbSize;
        private string m_ChosenHandler;
        private Client m_ConnectionModel;


        public SettingsModel()
        {
            m_OutputDirectory = "Output Directory:";
            m_SourceName = "Source Name:";
            m_LogName = "Log Name:";
            m_ThumbSize = "Thumbnail Size:";
            m_Directories = new ObservableCollection<string>();
            m_ConnectionModel = Client.Connection();
            m_ConnectionModel.start();
           
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

       
        public string OutputDirectory
        {
            get { return m_OutputDirectory; }
            set
            {
                m_OutputDirectory = "Output Directory:" + value;
                OnPropertyChanged("OutputDirectory");
            }
        }

        
        public string SourceName
        {
            get { return m_SourceName; }
            set
            {
                m_OutputDirectory = "Source Name:" + value;
                OnPropertyChanged("SourceName");
            }
        }

       
        public string LogName
        {
            get { return m_LogName; }
            set
            {
                m_LogName = "Log Name:" + value;
                OnPropertyChanged("LogName");
            }
        }

       
       
        public string ThumbSize
        {
            get { return m_ThumbSize; }
            set
            {
                m_OutputDirectory = "Thumbnail Size:" + value;
                OnPropertyChanged("ThumbSize");
            }
        }

       
       
        public string ChosenHandler
        {
            get { return m_ChosenHandler; }
            set
            {
                m_ChosenHandler = value;
                OnPropertyChanged("ChosenHandler");
            }
        }

       
        public ObservableCollection<string> Directories
        {
            get { return m_Directories; }
            set
            {
                m_Directories = value;
                OnPropertyChanged("Directories");
            }
        }

       
       

        public void sendToServer()
        {
            string[] args = { };
            CommandRecievedEventArgs command = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, args, "Empty");
            m_ConnectionModel.Sender(this, command);
        }
    }
}


using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Server;
using ImageService.Controller;
using ImageService.Modal;
using System.Configuration;

namespace ImageService3
{
    /// <summary>
    /// ImageService class.
    /// inherits from ServiceBase
    /// </summary>
    public partial class ImageService3 : ServiceBase
    {
        #region members
        private int eventId = 1;
        private ImageServer m_imageServer;          // The Image Server
        private IImageServiceModal modal;
        private IImageController controller;
        private ILoggingService logging;
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        #endregion


        /// <summary>
        /// Constructor of ImageService
        /// </summary>
        /// <param name="args"></param>
        public ImageService3(string[] args)
        {
            try
            {
                InitializeComponent();
                //read from app config
                string eventSourceName = ConfigurationManager.AppSettings.Get("SourceName"); 
                string logName = ConfigurationManager.AppSettings.Get("LogName");

                eventLog1 = new EventLog();
                if (!EventLog.SourceExists(eventSourceName))
                {
                    EventLog.CreateEventSource(eventSourceName, logName);
                }
                eventLog1.Source = eventSourceName;
                eventLog1.Log = logName;
                this.logging = new LoggingService();
                this.logging.MessageRecieved += WriteMessage;
                this.modal = new ImageServiceModal()
                {
                    OutputFolder = ConfigurationManager.AppSettings.Get("OutputDir"),
                    ThumbnailSize = int.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"))
                };
                this.controller = new ImageController(this.modal);
                this.m_imageServer = new ImageServer(this.controller, this.logging);
            }
            catch (Exception e)
            {
                this.eventLog1.WriteEntry(e.ToString(), EventLogEntryType.Error);
            }
        }

      
        /// <summary>
        /// The function manager when the service start.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart");
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            eventLog1.WriteEntry("Leave OnStart");

        }

        /// <summary>
        /// The function manager when the service stop.
        /// </summary>
        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
            this.m_imageServer.StopServer();
          
        }

        /// <summary>
        ///  The function manager when the service continue.
        /// </summary>
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }
        
       
        /// <summary>
        /// For each MessageTypeEnum match EventLogEntryType
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private EventLogEntryType GetType(MessageTypeEnum status)
        {
            switch (status)
            {
                case MessageTypeEnum.FAIL:
                    return EventLogEntryType.Error;
                case MessageTypeEnum.WARNING:
                    return EventLogEntryType.Warning;
                case MessageTypeEnum.INFO:
                default:
                    return EventLogEntryType.Information;
            }
        }

        /// <summary>
        /// The function write the log.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WriteMessage(Object sender, MessageRecievedEventArgs e)
        {
            eventLog1.WriteEntry(e.Message, GetType(e.Status));
        }
    }
}
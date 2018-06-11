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
using System.Collections.Generic;
using ImageService.Infrastructure.Enums;

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
        private ImageServer m_imageServer;          
        private IImageServiceModal modal;
        private IImageController controller;
        private ILoggingService logging;
        private TcpServer tcpServer;
        private List<Tuple<string, bool>> loggsMessages;
        private bool statusRun;

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
        /// ImageService function.
        /// </summary>
        /// <param name="args">command line args</param>
        public ImageService3(string[] args)
        {
            try
            {
                statusRun = true;
                InitializeComponent();
                //read params from app config
                string eventSourceName = ConfigurationManager.AppSettings.Get("SourceName");
                string logName = ConfigurationManager.AppSettings.Get("LogName");

                eventLog1 = new EventLog();
                if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
                }
                eventLog1.Source = eventSourceName;
                eventLog1.Log = logName;

                //initialize members
                this.logging = new LoggingService(this.eventLog1);
                this.logging.MessageRecieved += WriteMessage;


                string outPutFolder = ConfigurationManager.AppSettings.Get("OutputDir");

                this.modal = new ImageServiceModal(outPutFolder,this.statusRun.ToString())
                {
                    OutputFolder = ConfigurationManager.AppSettings.Get("OutputDir"),
                    ThumbnailSize = Int32.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"))
            
                };
                
                this.controller = new ImageController(this.modal, this.logging);
                this.m_imageServer = new ImageServer(controller, logging);
                this.controller.ImageServer = m_imageServer;
                IClientHandler ch = new ClientHandler(controller, logging);
                int port = 8000;
              
                tcpServer = new TcpServer(port, logging, ch);
                ImageServer.NotifyAllHandlerRemoved += tcpServer.NotifyAllClientsAboutUpdate;
                this.logging.UpdateLogEntries += tcpServer.NotifyAllClientsAboutUpdate;
            
                tcpServer.Start();
               
            }
            catch (Exception e)
            {
                this.eventLog1.WriteEntry(e.ToString(), EventLogEntryType.Error);
            }
        }


        /// <summary>
        /// OnStart function.
        /// manages what happens when the service is started
        /// </summary>
        /// <param name="args">command line args</param>
        protected override void OnStart(string[] args)
        {
           
            if (this.logging != null)
            {
                this.logging.InvokeUpdateEvent("In OnStart", MessageTypeEnum.INFO);
                statusRun = true;
            }
            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            eventLog1.WriteEntry("OnStart");
            if (this.logging != null)
            {
                this.logging.InvokeUpdateEvent("Leave OnStart", MessageTypeEnum.INFO);
            }

        }
        /// <summary>
        /// OnStop function.
        /// manages what happens when the service is sttopped
        /// </summary>
        protected override void OnStop()
        {
           
            if (this.logging != null)
            {
                this.logging.InvokeUpdateEvent("In onStop", MessageTypeEnum.INFO);
                statusRun = false;
            }
            this.m_imageServer.OnCloseServer();
            eventLog1.WriteEntry("Leave onStop.");
            if (this.logging != null)
            {
                this.logging.InvokeUpdateEvent("onStop", MessageTypeEnum.INFO);
            }
            this.tcpServer.Stop();
        }
        /// <summary>
        /// OnContinue function.
        /// manages what happens when the service is continue
        /// </summary>
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
            if (this.logging != null)
            {
                this.logging.InvokeUpdateEvent("In OnContinue.", MessageTypeEnum.INFO);
            }

        }

        public void OnDebug()
        {
            OnStart(null);
        }

       

        /// <summary>
        /// OnTimer function.
        /// </summary>
        /// <param name="sender">sender obj</param>
        /// <param name="args">arguments</param>
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            //eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }
        /// <summary>
        /// WriteMessage function.
        /// wrrites to log.
        /// </summary>
        /// <param name="sender"> sender obj</param>
        /// <param name="e" >MessageRecievedEventArgs obj</param>
        public void WriteMessage(Object sender, MessageRecievedEventArgs e)
        {
            eventLog1.WriteEntry(e.Message, GetType(e.Status));
        }

        /// <summary>
        /// GetType function.
        /// converts from MessageTypeEnum to EventLogEntryType.
        /// </summary>
        /// <param name="status">log type</param>
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
    }
}
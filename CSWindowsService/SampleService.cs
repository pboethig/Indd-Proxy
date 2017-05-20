/****************************** Module Header ******************************\
* Module Name:  SampleService.cs
* Project:      CSWindowsService
* Copyright (c) Microsoft Corporation.
* 
* Provides a sample service class that derives from the service base class - 
* System.ServiceProcess.ServiceBase. The sample service logs the service 
* start and stop information to the Application event log, and shows how to 
* run the main function of the service in a thread pool worker thread. 
* 
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/en-us/openness/resources/licenses.aspx#MPL.
* All other rights reserved.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

#region Using directives
using System.ServiceProcess;
using Indd.Service.Log;
using System.Collections.Generic;

#endregion

namespace CSWindowsService
{
    public partial class SampleService : ServiceBase
    {
        /// <summary>
        /// Watcher
        /// </summary>
        private Indd.Service.Filesystem.Watcher watcher;

       private  List<Indd.Service.Filesystem.Watcher> watcherList = new List<Indd.Service.Filesystem.Watcher>();
        
       Indd.Service.IndesignServerWrapper.ApplicationMananger ApplicationManager = new Indd.Service.IndesignServerWrapper.ApplicationMananger();

        public SampleService()
        {
            InitializeComponent();

        }


        /// <summary>
        /// The function is executed when a Start command is sent to the 
        /// service by the SCM or when the operating system starts (for a 
        /// service that starts automatically). It specifies actions to take 
        /// when the service starts. In this code sample, OnStart logs a 
        /// service-start message to the Application log, and queues the main 
        /// service function for execution in a thread pool worker thread.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <remarks>
        /// A service application is designed to be long running. Therefore, 
        /// it usually polls or monitors something in the system. The 
        /// monitoring is set up in the OnStart method. However, OnStart does 
        /// not actually do the monitoring. The OnStart method must return to 
        /// the operating system after the service's operation has begun. It 
        /// must not loop forever or block. To set up a simple monitoring 
        /// mechanism, one general solution is to create a timer in OnStart. 
        /// The timer would then raise events in your code periodically, at 
        /// which time your service could do its monitoring. The other 
        /// solution is to spawn a new thread to perform the main service 
        /// functions, which is demonstrated in this code sample.
        /// </remarks>
        protected override void OnStart(string[] args)
        {
            try
            {
                ApplicationManager.createInstance();

                Indd.Service.Config.Factory configFactory = new Indd.Service.Config.Factory();

                string jobIn = configFactory.getJobQueuePath("in");

                string jobIn2 = configFactory.getJobQueuePath("in2");

                watcher = new Indd.Service.Filesystem.Watcher(jobIn);

                watcher = new Indd.Service.Filesystem.Watcher(jobIn2);

                watcherList.Add(watcher);
                
                // Process the list of files found in the directory. 
                string[] fileEntries = System.IO.Directory.GetFiles((string)jobIn);

                string[] fileEntries2 = System.IO.Directory.GetFiles((string)jobIn2);

                this.touchFiles(fileEntries);

                this.touchFiles(fileEntries2);

                Syslog.log("Filesystem-Watcher started now. Watch: " + jobIn, System.Diagnostics.EventLogEntryType.SuccessAudit);
            }
            catch (System.Exception ex)
            {
                Syslog.log(ex.Message);
            }
        }
        
        /// <summary>
        /// Touches files
        /// </summary>
        /// <param name="fileEntries"></param>
        void touchFiles(string[] fileEntries)
        {
            string path = "";
            
            try
            {
                foreach (string fileName in fileEntries)
                {
                    path = fileName;

                    System.IO.File.SetCreationTime(fileName, System.DateTime.Now);
                }
            }
            catch (System.Exception ex)
            {
                Syslog.log("Filesystem-Watcher : Error on setting filetime: " + System.DateTime.Now.ToString() + " Path:" + path + " Exeption:" + ex.Message);
            }

        }

        /// <summary>
        /// The function is executed when a Stop command is sent to the 
        /// service by SCM. It specifies actions to take when a service stops 
        /// running. In this code sample, OnStop logs a service-stop message 
        /// to the Application log, and waits for the finish of the main 
        /// service function.
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                
                foreach(Indd.Service.Filesystem.Watcher watcher in this.watcherList)
                {
                    watcher.watcher.EnableRaisingEvents = false;
                }


                Syslog.log("Stopped Indd Service", System.Diagnostics.EventLogEntryType.SuccessAudit);
                
                ApplicationManager.createInstance().Quit();
            }
            catch(System.Exception ex)
            {
                Syslog.log(ex.Message);
            }
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Artemis_Loader.Core;

namespace Artemis_Loader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex mutex;

        public App()
        {
            mutex = new Mutex(false, "ArtemisLoader");
            
            if (!mutex.WaitOne(0, false))
            {
                MessageDispatcher.Dispatch(new() { Type = MessageType.BringToForeground });
                Current.Shutdown(-1);
                return;
            }
        }

        ~App()
        {
            mutex.ReleaseMutex();
            mutex.Dispose();
        }
    }
}

using DesktopClient.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopClient.Proxies
{
    class EventProxy
    {
        #region Singleton
        private static EventProxy instance = null;
        private static readonly object padlock = new object();

        EventProxy()
        {
        }

        public static EventProxy Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new EventProxy();
                    }
                    return instance;
                }
            }
        }
        #endregion

        public EventHandler ViewChanged { get; set; }
        public EventHandler LoggedIn { get; set; }

        public void RaiseViewChange(PictureViewerState targetState)
        {
            ViewChanged.Invoke(targetState, EventArgs.Empty);
        }

        public void RaiseLoggerIn()
        {
            LoggedIn.Invoke(null, EventArgs.Empty);
        }

    }
}

using Core.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopClient.Services
{
    class SelectedPictureService
    {
        #region Singleton
        private static SelectedPictureService instance = null;
        private static readonly object padlock = new object();

        SelectedPictureService()
        {
        }

        public static SelectedPictureService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SelectedPictureService();
                    }
                    return instance;
                }
            }
        }
        #endregion

        public PictureOutputDto SelectedPicture { get; set; }
    }
}

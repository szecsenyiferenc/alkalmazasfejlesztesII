using Core.Models;
using Core.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Services
{
    class LoginService
    {
        #region Singleton
        private static LoginService instance = null;
        private static readonly object padlock = new object();

        LoginService()
        {
            _restBackendService = RestBackendService.Instance;
        }

        public static LoginService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LoginService();
                    }
                    return instance;
                }
            }
        }
        #endregion

        private readonly RestBackendService _restBackendService;

        public CustomerOutputDto CurrentUser { get; set; }

        public async Task<bool> CheckLogin(LoginData loginData)
        {
            CurrentUser = await _restBackendService.CheckLogin(loginData);
            if(CurrentUser == null)
            {
                return false;
            }
            return true;
        }
    }
}

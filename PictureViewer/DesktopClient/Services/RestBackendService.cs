using Core.Models;
using Core.Models.Input;
using Core.Models.Output;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesktopClient.Services
{
    class RestBackendService
    {
        #region Singleton
        private static RestBackendService instance = null;
        private static readonly object padlock = new object();

        RestBackendService()
        {
            _client = new HttpClient();
        }

        public static RestBackendService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RestBackendService();
                    }
                    return instance;
                }
            }
        }
        #endregion

        private readonly string _baseUrl = "http://localhost:5000";
        private readonly HttpClient _client;

        public async Task<CustomerOutputDto> CheckLogin(LoginData loginData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(loginData); // or JsonSerializer.Serialize if using System.Text.Json
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                var response = await _client.PostAsync($"{_baseUrl}/api/customers/checklogin", stringContent);

                var responseString = await response.Content.ReadAsStringAsync();

                var customer = JsonConvert.DeserializeObject<CustomerOutputDto>(responseString);

                return customer;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task Register(CustomerInputDto customer)
        {

            var json = JsonConvert.SerializeObject(customer); // or JsonSerializer.Serialize if using System.Text.Json
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
            var response = await _client.PostAsync($"{_baseUrl}/api/customers", stringContent);
        }

        public async Task DeleteCustomer()
        {
            var user = LoginService.Instance.CurrentUser;

            await _client.DeleteAsync($"{_baseUrl}/api/customers/{user.Id}");
        }

        public async Task<IEnumerable<PictureOutputDto>> GetPictures()
        {
            try
            {
                string response = await _client.GetStringAsync($"{_baseUrl}/api/pictures");

                var pictures = JsonConvert.DeserializeObject<IEnumerable<PictureOutputDto>>(response);

                return pictures;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task UploadPicture(PictureInputDto picture)
        {
            try
            {
                var json = JsonConvert.SerializeObject(picture); // or JsonSerializer.Serialize if using System.Text.Json
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                var user = LoginService.Instance.CurrentUser;

                var response = await _client.PostAsync($"{_baseUrl}/api/customers/{user.Id}/pictures", stringContent);

                var responseString = await response.Content.ReadAsStringAsync();

                var customer = JsonConvert.DeserializeObject<CustomerOutputDto>(responseString);
            }
            catch (HttpRequestException e)
            {
            }
        }

        public async Task UpdatePicture(PictureInputDto picture)
        {
            try
            {
                var json = JsonConvert.SerializeObject(picture); // or JsonSerializer.Serialize if using System.Text.Json
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                int id  = SelectedPictureService.Instance.SelectedPicture.Id;

                var response = await _client.PutAsync($"{_baseUrl}/api/customers/{id}", stringContent);
            }
            catch (HttpRequestException e)
            {
            }
        }

        public async Task DeletePicture(PictureOutputDto picture)
        {
            try
            {
                await _client.DeleteAsync($"{_baseUrl}/api/delete/{picture.Id}");
            }
            catch (HttpRequestException e)
            {
            }
        }

    }
}

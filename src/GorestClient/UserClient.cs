using System;
using System.Net.Http;
using System.Net.Http.Headers;
using EmployeeManager.Data;
using EmployeeManager.Data.Common;
using GorestClient.Data;
using GorestClient.Eextensions;
using GorestClient.Extensions;

namespace GorestClient
{
    public class UserClient : IUserClient
    {
        private IClientConfig _config;
        private string _url => $"{_config.BaseUrl}/users";
        private HttpClient _client;

        public UserClient(IClientConfig config)
        {
            _config = config;
            InitClient();
        }

        public PagedResponse<User[]> GetAll(int page)
        {
            return SendGetRequest<PagedResponse<User[]>>($"?page={page}");
        }

        public PagedResponse<User[]> GetByName(string name)
        {
            return SendGetRequest<PagedResponse<User[]>>($"?name={name}");
        }

        public User GetById(int id)
        {
            return SendGetRequest<PagedResponse<User>>($"/{id}").Data;
        }

        public ActionResult<User> Create(User user)
        {
            var content = user.GetFormUrlEncodedContent();
            var response = _client.PostAsync(_url, content);

            return response.GetActionResult<User>();
        }

        public ActionResult<User> Update(User user)
        {
            var content = user.GetFormUrlEncodedContent();
            var response = _client.PutAsync($"{_url}/{user.Id}", content);

            return response.GetActionResult<User>();
        }

        public ActionResult Delete(int id)
        {
            var response = _client.DeleteAsync($"{_url}/{id}");
            
            return response.GetActionResult<User>();
        }

        private TResult SendGetRequest<TResult>(string parameters)
            where TResult : class, new()
        {
            var response = _client.GetAsync($"{_url}/{parameters}");

            return response.DeserializeTo<TResult>();
        }

        private void InitClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.APIToken);
        }
    }
}

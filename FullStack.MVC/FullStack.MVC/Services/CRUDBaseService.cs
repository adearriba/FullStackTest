using FullStack.MVC.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FullStack.MVC.Services
{
    public class CRUDBaseService<T> : APIBaseService, ICRUDBaseService<T>
    {
        protected readonly string _endpoint = String.Empty;
        public CRUDBaseService(HttpClient httpClient, string endpoint)
            : base(httpClient)
        {
            _apiUri = $"{_apiUri}/{endpoint}";
        }

        public async Task<T> AddAsync(T item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUri, content);

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseJson);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var responseString = await _httpClient.GetStringAsync(_apiUri);
            var data = JsonConvert.DeserializeObject<List<T>>(responseString);

            return data;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var responseString = await _httpClient.GetStringAsync($"{_apiUri}/{id}");
            var data = JsonConvert.DeserializeObject<T>(responseString);

            return data;
        }

        public async Task RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUri}/{id}");
            var responseJson = await response.Content.ReadAsStringAsync();
        }

        public async Task UpdateAsync(T item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUri}", content);
            var responseJson = await response.Content.ReadAsStringAsync();
        }
    }
}

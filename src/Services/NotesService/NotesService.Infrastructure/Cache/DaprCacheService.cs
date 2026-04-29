using NotesService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapr.Client;

namespace NotesService.Infrastructure.Cache
{
    public class DaprCacheService : ICacheService
    {
        private readonly DaprClient _daprClient;
        private const string STORE_NAME = "statestore";

        public DaprCacheService(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            return await _daprClient.GetStateAsync<T>(STORE_NAME, key);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            await _daprClient.SaveStateAsync(STORE_NAME, key, value);
        }

        public async Task RemoveAsync(string key)
        {
            await _daprClient.DeleteStateAsync(STORE_NAME, key);
        }
    }
}

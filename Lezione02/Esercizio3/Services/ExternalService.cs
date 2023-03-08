namespace Esercizio3.Services
{
    public class ExternalService
    {
        private readonly HttpClient _client;

        public ExternalService(HttpClient client)
        {
            _client = client;

            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        }

        public async Task<string> GetTodosById(int Id)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, $"todos/{Id}");

            var response = await _client.SendAsync(req);

            string json = await response.Content.ReadAsStringAsync();
            return json;
        }
    }
}

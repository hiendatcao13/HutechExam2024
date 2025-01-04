namespace Hutech.Exam.Client.BUS
{
    public class ApiService
    {
        private HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}

using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace TestMVC.Services
{
    public class APIHander
    {
        public IHttpContextAccessor? Context { get; set; }
        public APIHander(IHttpContextAccessor? http) 
        {
            http = Context;
        }

        public HttpResponseMessage ApiCall(string url,dynamic body) 
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7280");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization(new AuthenticationHeaderValue("Bearer ", body.Token));
            StringContent content =new(JsonConvert.SerializeObject(body),Encoding.UTF8,"application/json");
            var task=Task.Run(() => client.PostAsync("/api/" + url, content));
            task.Wait();
            HttpResponseMessage response = task.Result;
            return response;
        }

        public HttpResponseMessage ApiCallForEdit(string url, dynamic body)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7280");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization(new AuthenticationHeaderValue("Bearer ", body.Token));
            StringContent content = new(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var task = Task.Run(() => client.PostAsync("/api/" + url, content));
            task.Wait();
            HttpResponseMessage response = task.Result;
            return response;
        }
    }
}

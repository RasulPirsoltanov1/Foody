using Foody.WebUI.Dtos.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Foody.WebUI.ViewComponents
{
    public class _ServiceDefaultComponentPartial : ViewComponent
    {
        IHttpClientFactory _httpClientFactory;

        public _ServiceDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response =await client.GetAsync("https://localhost:7137/api/Services/");
            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var values =JsonSerializer.Deserialize<List<ResultServiceDto>>(data,options);
                return View(values);
            }
            return View();
        }
    }
}

using Foody.WebUI.Dtos.EventDtos;
using Foody.WebUI.Dtos.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Foody.WebUI.ViewComponents
{
    public class _EventDefaultComponentPartial:ViewComponent
    {
        IHttpClientFactory _httpClientFactory;

        public _EventDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7137/api/YummyEvents/");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var values = JsonSerializer.Deserialize<List<ResultYummyEventDto>>(data, options);
                    return View(values);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(null);
        }
    }
}

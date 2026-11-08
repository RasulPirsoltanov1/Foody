using Foody.WebUI.Dtos.CategoryDtos;
using Foody.WebUI.Dtos.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Foody.WebUI.ViewComponents.MenuDefaultComponents
{
    public class _MenuCategoryDefaultComponentPartial : ViewComponent
    {
        IHttpClientFactory _httpClientFactory;

        public _MenuCategoryDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7137/api/Categories/");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var values = JsonSerializer.Deserialize<List<ResultCategoryDto>>(data, options);
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

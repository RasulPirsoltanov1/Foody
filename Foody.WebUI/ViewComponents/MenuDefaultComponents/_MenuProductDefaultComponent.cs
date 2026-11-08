using Foody.WebUI.Dtos.CategoryDtos;
using Foody.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Foody.WebUI.ViewComponents.MenuDefaultComponents
{
    public class _MenuProductDefaultComponentPartial : ViewComponent
    {
        IHttpClientFactory _httpClientFactory;

        public _MenuProductDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7137/api/Products/");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var values = JsonSerializer.Deserialize<List<ResultProductDto>>(data, options);
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

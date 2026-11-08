using Foody.WebUI.Dtos.ProductDtos;
using Foody.WebUI.Dtos.TestimonialDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Foody.WebUI.ViewComponents
{
    public class _TestimonialDefaultComponentPartial:ViewComponent
    {
        IHttpClientFactory _httpClientFactory;

        public _TestimonialDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7137/api/Testimonials/");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var values = JsonSerializer.Deserialize<List<ResultTestimonialDtoUI>>(data, options);
                    return View(values ?? new List<ResultTestimonialDtoUI>());
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

using System.Xml.Linq;

namespace Foody.WebApi.Entites
{
    public class Feature
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string VideUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}

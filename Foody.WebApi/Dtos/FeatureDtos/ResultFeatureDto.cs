namespace Foody.WebApi.Dtos.FeatureDtos
{
    public class ResultFeatureDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string VideUrl { get; set; }
        public string ImageUrl { get; set; }
    }

    public class GetByIdFeatureDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string VideUrl { get; set; }
        public string ImageUrl { get; set; }
    }
    public class UpdateFeatureDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string VideUrl { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CreateFeatureDto
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string VideUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}

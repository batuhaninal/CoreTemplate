using Application.Models.RequestParameters.Commons;
using System.Text.Json.Serialization;

namespace Application.Models.RequestParameters.Articles
{
    public class ArticleRequestParameter : BaseRequestParameter
    {
        public Guid? CategoryId { get; set; }
        [JsonIgnore]
        public List<Guid>? CategoryIds { get; set; }
    }
}
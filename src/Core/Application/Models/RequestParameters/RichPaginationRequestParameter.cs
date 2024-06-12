using Application.Models.RequestParameters.Commons;

namespace Application.Models.RequestParameters
{
    public class RichPaginationRequestParameter : BasePaginationRequestParameter
    {
        protected override int MaxSize { get; set; } = 250;
        protected override int DefaultSize { get; set; } = 50;
    }
}

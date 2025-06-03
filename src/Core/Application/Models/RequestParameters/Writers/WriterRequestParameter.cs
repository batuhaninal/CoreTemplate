using Application.Models.RequestParameters.Commons;

namespace Application.Models.RequestParameters.Writers
{
    public class WriterRequestParameter : BaseRequestParameter
    {
        public Int16[]? Levels { get; set; }
    }
}

namespace Application.Models.RequestParameters.Commons
{
    public abstract class BaseRequestParameter : BasePaginationRequestParameter
    {
        private string? _condition;
        public virtual string? Condition 
        { 
            get => _condition; 
            set => _condition = value?.TrimStart().TrimEnd().ToLower();    
        }
        public string? OrderBy { get; set; }
        public virtual DateTime? MinDate { get; set; }
        public virtual DateTime? MaxDate { get; set; }
        public virtual bool? IsActive { get; set; }
    }
}
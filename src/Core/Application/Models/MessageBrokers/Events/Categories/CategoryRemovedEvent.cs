namespace Application.Models.MessageBrokers.Events.Categories
{
    public class CategoryRemovedEvent
    {
        public string IndexName { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
    }
}

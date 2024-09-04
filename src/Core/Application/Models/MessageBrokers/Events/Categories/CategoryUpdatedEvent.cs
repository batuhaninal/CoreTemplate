namespace Application.Models.MessageBrokers.Events.Categories
{
    public class CategoryUpdatedEvent
    {
        public string IndexName { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public string Model { get; set; } = null!;
    }
}

namespace Application.Models.MessageBrokers.Events.Categories
{
    public class CategoryCreatedEvent 
    {
        public string IndexName { get; set; } = null!;
        public string Model { get; set; } = null!;
    }
}

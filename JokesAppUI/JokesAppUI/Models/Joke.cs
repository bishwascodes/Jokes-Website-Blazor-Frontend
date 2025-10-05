namespace JokesAppUI.Models
{
    public class Joke
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public List<string> Audiences { get; set; } = new();
    }

    public class NewJokeRequest
    {
        public string Content { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public List<int> AudienceIds { get; set; } = new();
    }
}
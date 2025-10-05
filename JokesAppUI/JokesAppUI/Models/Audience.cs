namespace JokesAppUI.Models
{
    public class Audience
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public List<string> Jokes { get; set; } = new();
    }

    public class NewAudienceRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}

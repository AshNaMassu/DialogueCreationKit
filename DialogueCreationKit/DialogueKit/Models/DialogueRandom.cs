namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class DialogueRandom
    {
        public Guid Id { get; set; }
        public List<string> Content { get; set; }

        public DialogueRandom()
        {
            Id = Guid.NewGuid();
            Content = new List<string>();
        }
    }
}

namespace DialogueCreationKit.DialogueKit.Models
{
    public class DialogueMessageCheck
    {
        public Guid Id { get; set; }
        public string Value { get; set; }   
        public string Infinitive { get; set; }
        public List<string> Variants { get; set; }

        public DialogueMessageCheck() 
        {
            Id = Guid.NewGuid();
            Value = string.Empty;
            Infinitive = string.Empty;
            Variants = new List<string>();
        }
    }
}

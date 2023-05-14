using Microsoft.AspNetCore.Identity;

namespace DialogueCreationKit.DialogueKit.Models
{
    public class DialogueMessageCheck
    {
        public Guid Id { get; set; }
        public bool IsInfinitive { get; set; }
        public string Value { get; set; }   
        public string Infinitive { get; set; }
        public List<Variant> Variants { get; set; }

        public DialogueMessageCheck() 
        {
            Id = Guid.NewGuid();
            Value = string.Empty;
            Infinitive = string.Empty;
            Variants = new();
        }
    }

    public class Variant
    {
        public string Value { get; set; }

        public Variant(string v) 
        {
            Value = v;
        }
    }
}

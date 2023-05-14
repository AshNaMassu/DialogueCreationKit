using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class Npc
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Npc() 
        {
            Id = Guid.NewGuid();
            Name = "ABCDEF"[new Random().Next(0, 6)].ToString();
        }

        public override string ToString()
        {
            return Name + $" ({Id})";
        }
    }
}

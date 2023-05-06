using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class Npc
    {
        [Required, DisplayName("Имя персонажа")]
        public string Name { get; set; }

        [Required, DisplayName("Идентификатор")]
        public Guid Id { get; set; }

        public bool IsActor { get; set; }
        public Npc() 
        {
            Id = Guid.NewGuid();
            Name = "ABCDEF"[new Random().Next(0, 6)].ToString();
            IsActor = false;
        }
    }
}

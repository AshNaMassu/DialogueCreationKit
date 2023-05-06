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
        public Guid? Id { get; set; }

        public bool IsFisrt { get; set; }

        public Npc() 
        {
            Id = null;
            Name = string.Empty;
            IsFisrt = false;
        }
    }
}

using DialogueCreationKit.DialogueKit.Enums;

namespace DialogueCreationKit.DialogueKit.Models
{
    public class DialogueCreationModel
    {
        public List<Npc> ListNpc { get; set; } = new List<Npc>() { new Npc(), new Npc() };

        public string Content { get; set; } = string.Empty;
        public DialogueCreateMode Mode { get; set; }
    }
}

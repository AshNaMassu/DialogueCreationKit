using DialogueCreationKit.DialogueKit.Enums;

namespace DialogueCreationKit.DialogueKit.Models
{
    public class DialogueCreationModel
    {
        public List<Npc> ListNpc { get; set; } = new List<Npc>() { new Npc(), new Npc() };

        public string Content { get; set; } = string.Empty;
        public DialogueCreateMode Mode { get; set; }

        public List<string> ListMessages { get; set; } = new();
        public List<DialogueMessage> DialogueMessages { get; set; } = new();
    }
}

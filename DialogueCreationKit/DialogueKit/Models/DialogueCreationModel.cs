using DialogueCreationKit.DialogueKit.Enums;

namespace DialogueCreationKit.DialogueKit.Models
{
    public class DialogueCreationModel
    {
        public Npc Actor { get; set; }
        public Npc Companion { get; set; }

        public string Content { get; set; }
        public DialogueCreateMode Mode { get; set; }

        public List<DialogueMessageView> ListMessages { get; set; }

        public DialogueCreationModel() 
        {
            Actor = new Npc() { IsActor = true };
            Companion = new Npc();
            Content = string.Empty;
            Mode = DialogueCreateMode.Dialogue;
            ListMessages = new List<DialogueMessageView>();
        }
    }
}

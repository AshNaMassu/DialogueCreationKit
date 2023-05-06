using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.DialogueKit.Models
{
    public class DialogueMessageView
    {
        public EventCallback OnMessageClick { get; set; }
        public Npc Npc { get; set; }
        public DialogueMessage Message { get; set; }

        public DialogueNode Node { get; set; }

        public DialogueMessageView()
        {
            Npc = new Npc();
            Message = new DialogueMessage();
            Node = new DialogueNode();
        }

        public DialogueMessageView(string message) : this()
        {
            Message.Content = message;
        }
    }
}

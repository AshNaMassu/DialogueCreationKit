using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.DialogueKit.Models
{
    public class DialogueMessageView
    {
        public int Id { get; set; }
        public EventCallback OnMessageClick { get; set; }
        
        
        public DialogueMessage Message { get; set; }
        /*
        public Npc Npc { get; set; }
        public DialogueNode Node { get; set; }
        public int Theme { get; set; }
        */
        public DialogueMessageView()
        {
            Message = new DialogueMessage();
            //Npc = new Npc();
            //Node = new DialogueNode();
            //Theme = 0;
        }

        public DialogueMessageView(int id , string message) : this()
        {
            Id = id;
            Message.Content = message;
        }
    }
}

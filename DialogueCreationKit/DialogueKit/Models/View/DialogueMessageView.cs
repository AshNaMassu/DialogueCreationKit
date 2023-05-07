using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.DialogueKit.Models.View
{
    public class DialogueMessageView
    {
        public int Id { get; set; }
        public DialogueMessage Message { get; set; }
        public DialogueMessageView()
        {
            Message = new DialogueMessage();
        }

        public DialogueMessageView(int id, string message) : this()
        {
            Id = id;
            Message.Content = message;
        }
    }
}

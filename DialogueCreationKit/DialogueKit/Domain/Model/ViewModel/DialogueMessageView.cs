using DialogueCreationKit.DialogueKit.Infrastructure.Helpers;

namespace DialogueCreationKit.DialogueKit.Domain.Model.ViewModel
{
    public class DialogueMessageView
    {
        public int Id { get; set; }
        //public DialogueMessage Message { get; set; }
        public string Message { get; set; }
        public DialogueMessageView()
        {
            //Message = new DialogueMessage();
            Message = string.Empty;
        }

        public DialogueMessageView(int id, string message) : this()
        {
            Id = id;
            Message = message;
        }

        public DialogueMessageView Copy()
        {
            return new DialogueMessageView()
            {
                Id = Id,
                Message = Message.Copy()
            };
        }
    }
}

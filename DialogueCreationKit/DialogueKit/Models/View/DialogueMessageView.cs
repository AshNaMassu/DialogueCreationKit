

namespace DialogueCreationKit.DialogueKit.Models.View
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
                Message = new string(Message.Select(x => x).ToArray())
            };
        }
    }
}

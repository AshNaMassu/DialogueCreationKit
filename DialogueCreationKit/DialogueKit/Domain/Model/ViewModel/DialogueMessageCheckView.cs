namespace DialogueCreationKit.DialogueKit.Domain.Model.ViewModel
{
    public class DialogueMessageCheckView
    {
        public DialogueMessageView Message { get; set; }
        public List<DialogueMessageCheck> Checks { get; set; }

        public DialogueMessageCheckView()
        {
            Message = new();
            Checks = new();
        }

        public DialogueMessageCheckView(DialogueMessageView message)
        {
            Message = message.Copy();
            Checks = new();
        }
    }
}

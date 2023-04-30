using DialogueCreationKit.DialogueKit.Models.Enums;

namespace DialogueCreationKit.DialogueKit.Models.Dialogue
{
    [Serializable]
    public class DialogueMessage
    {
        public Guid? Id { get; set; }
        public DialogueStage Stage { get; set; }
        public bool IsActive { get; set; }
        public string MessageContent { get; set; }


        public DialogueMessage()
        {
            Id = null;
            Stage = DialogueStage.None;
            IsActive = true;
            MessageContent = string.Empty;
        }
    }


}

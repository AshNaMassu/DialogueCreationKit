
using DialogueCreationKit.Dialogue.Models.Enums;

namespace DialogueCreationKit.Dialogue.Models
{
    [Serializable]
    public class DialogueMessage
    {
        public string Id { get; set; }
        public DialogueStage Stage { get; set; }
        public bool IsActive { get;set; }
        public string MessageContent { get; set; }
        

        public DialogueMessage()
        {
            Id = string.Empty;
            Stage = DialogueStage.None;
            IsActive = true;
            MessageContent = string.Empty;
        }
    }

    
}

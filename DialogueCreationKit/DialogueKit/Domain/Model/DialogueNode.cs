using DialogueCreationKit.DialogueKit.Domain.Enums;

namespace DialogueCreationKit.DialogueKit.Domain.Model
{
    [Serializable]
    public class DialogueNode
    {
        public Guid Id { get; set; }
        public DialogueStage Stage { get; set; }
        public bool IsCheckable { get; set; }
        public string Message { get; set; }
        public Guid? Child { get; set; }


        public DialogueNode()
        {
            Id = Guid.NewGuid();
            Stage = DialogueStage.None;
            //IsActive = true;
            Message = string.Empty;
        }
    }


}

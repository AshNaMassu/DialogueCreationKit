using DialogueCreationKit.DialogueKit.Models.Enums;

namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class DialogueNode
    {
        public Guid? Id { get; set; }
        public DialogueStage Stage { get; set; }
        public bool IsActive { get; set; }
        public Guid MessageId { get; set; }
        public List<Guid> Childs { get; set; }


        public DialogueNode()
        {
            Id = null;
            Stage = DialogueStage.None;
            IsActive = true;
        }
    }


}

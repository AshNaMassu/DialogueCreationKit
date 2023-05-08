namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class DialogueStorage
    {
        public DialogueTree Tree { get; set; }
        public List<DialogueNode> Nodes { get; set; }
    }
}

namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class DialogueStorage
    {
        public string ActorName { get; set; }
        public DialogueTree Tree { get; set; }
        public List<DialogueNode> Nodes { get; set; }
    }
}

namespace DialogueCreationKit.DialogueKit.Domain.Model
{
    [Serializable]
    public class DialogueStorage
    {
        public string ActorName { get; set; }
        public DialogueTree Tree { get; set; }
        public List<DialogueNode> Nodes { get; set; }
        public List<DialogueMessageCheck> Checks { get; set; }
    }
}

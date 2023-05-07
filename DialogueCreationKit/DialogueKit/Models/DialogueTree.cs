namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class DialogueTree
    {
        public Guid Id { get; set; }
        public Npc Npc { get; set; }
        public DialogueNode Begin { get; set; }
        public List<DialogueNode> Content { get; set; }
        public DialogueNode End { get; set; }

        public DialogueTree()
        {
            Id = Guid.NewGuid();
            Npc = new Npc();
            Begin = new DialogueNode();
            Content = new List<DialogueNode>();
            End = new DialogueNode();
        }
    }
}

namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class DialogueTree
    {
        public Npc Npc;
        public DialogueNode Begin;
        public List<DialogueNode> Content;
        public DialogueNode End;

        public DialogueTree()
        {
            Npc = new Npc();
            Begin = new DialogueNode();
            Content = new List<DialogueNode>();
            End = new DialogueNode();
        }
    }
}

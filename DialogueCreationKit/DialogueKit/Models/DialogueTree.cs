namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class DialogueTree
    {
        public Npc Npc;
        public DialogueNode Root;

        public DialogueTree()
        {
            Npc = new Npc();
            Root = new DialogueNode();
        }
    }
}

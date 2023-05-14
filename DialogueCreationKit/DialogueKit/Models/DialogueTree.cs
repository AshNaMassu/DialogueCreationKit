namespace DialogueCreationKit.DialogueKit.Models
{
    [Serializable]
    public class DialogueTree
    {
        //public Guid Id { get; set; }
        public Npc Npc { get; set; }
        public Guid Begin { get; set; }
        public List<Guid> Content { get; set; }
        public Guid End { get; set; }

        public DialogueTree()
        {
           // Id = Guid.NewGuid();
            Npc = new Npc();
            Content = new List<Guid>();
        }
    }
}

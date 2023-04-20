using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace DialogueCreationKit.Dialogue.Models
{
    public class DialogueNodeModel : NodeModel
    {
        public DialogueNodeModel(Point position = null) : base(position, RenderLayer.HTML)
        {
            DialogueMessages = new List<DialogueMessage>
            {
                new DialogueMessage
                {
                    Name = "Id",
                    Type = ColumnType.Integer,
                    Primary = true
                },
                new DialogueMessage
                {
                    Name = "Test",
                    Type = ColumnType.Integer
                }
            };

            AddPort(DialogueMessages[0], PortAlignment.Right);
            AddPort(DialogueMessages[1], PortAlignment.Left);
        }

        public string Name { get; set; } = "DialogueNodeModel";
        public List<DialogueMessage> DialogueMessages { get; }
        public bool HasPrimaryColumn => DialogueMessages.Any(c => c.IdMessage);

        public DialogueNodePort GetPort(DialogueMessage dialogueMessage) => Ports.Cast<DialogueNodePort>().FirstOrDefault(p => p.DialogueMessage == column);

        public void AddPort(Column column, PortAlignment alignment) => AddPort(new DialogueNodePort(this, column, alignment));
    }
}

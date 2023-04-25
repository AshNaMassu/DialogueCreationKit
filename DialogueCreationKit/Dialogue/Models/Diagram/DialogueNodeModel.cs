using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using DialogueCreationKit.Dialogue.Models.Enums;

namespace DialogueCreationKit.Dialogue.Models.Diagram
{
    public class DialogueNodeModel : NodeModel
    {
        public DialogueNodeModel(DialogueMessageView dialogueMessage, Point position = null) : base(position, RenderLayer.HTML)
        {
            if (dialogueMessage == null)
                throw new ArgumentNullException(nameof(dialogueMessage));
            else
                DialogueMessage = dialogueMessage;

            if (DialogueMessage.Stage != DialogueStage.Begin)
                AddPort(DialogueMessage, PortAlignment.Left);
            
            if (DialogueMessage.Stage != DialogueStage.End && DialogueMessage.ChildsNodes != null)
            {
                foreach (var child in DialogueMessage.ChildsNodes)
                    AddPort(child, PortAlignment.Right);
            }
                
        }

        public DialogueMessageView DialogueMessage { get; }

        public DialoguePortModel GetPort(DialogueMessageView dialogueMessage) => Ports.Cast<DialoguePortModel>().FirstOrDefault(p => p.DialogueMessage.Id.Equals(dialogueMessage.Id));

        public void AddPort(DialogueMessageView dialogueMessage, PortAlignment alignment) => AddPort(new DialoguePortModel(this, dialogueMessage, alignment));
    }
}

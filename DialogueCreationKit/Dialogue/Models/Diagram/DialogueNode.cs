using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using DialogueCreationKit.Dialogue.Models.Enums;

namespace DialogueCreationKit.Dialogue.Models.Diagram
{
    public class DialogueNode : NodeModel
    {
        public DialogueNode(DialogueMessage dialogueMessage, Point position = null) : base(position, RenderLayer.HTML)
        {
            if (dialogueMessage == null)
                DialogueMessage = new DialogueMessage();
            else
                DialogueMessage = dialogueMessage;

            if (DialogueMessage.Stage != DialogueStage.Begin)
                AddPort(DialogueMessage, PortAlignment.Right);
            
            if (DialogueMessage.Stage != DialogueStage.End && DialogueMessage.ChildsNodes != null)
            {
                foreach (var  child in DialogueMessage.ChildsNodes)
                    AddPort(DialogueMessage, PortAlignment.Left);
            }
                
        }

        public DialogueMessage DialogueMessage { get; }

        public DialoguePort GetPort(DialogueMessage dialogueMessage) => Ports.Cast<DialoguePort>().FirstOrDefault(p => p.DialogueMessage.Id.Equals(dialogueMessage.Id));

        public void AddPort(DialogueMessage dialogueMessage, PortAlignment alignment) => AddPort(new DialoguePort(this, dialogueMessage, alignment));
    }
}

using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Newtonsoft.Json;

namespace DialogueCreationKit.Dialogue.Models
{
    public class DialogueNodePort : PortModel
    {
        public DialogueNodePort(NodeModel parent, DialogueMessage dialogueMessage, PortAlignment alignment = PortAlignment.Bottom)
            : base(parent, alignment, null, null)
        {
            DialogueMessage = dialogueMessage;
        }

        [JsonIgnore]
        public DialogueMessage DialogueMessage { get; }

        public override bool CanAttachTo(PortModel port)
        {
            // Avoid attaching to self port/node
            if (!base.CanAttachTo(port))
                return false;

            var targetPort = port as DialogueNodePort;
            var targetDialogueMessage = targetPort.DialogueMessage;

            if (DialogueMessage.Type != targetDialogueMessage.Type)
                return false;

            if (DialogueMessage.Primary && targetDialogueMessage.Primary)
                return false;

            if (DialogueMessage.Primary && targetPort.Links.Count > 0 ||
                targetDialogueMessage.Primary && Links.Count > 1) // Ongoing link
                return false;

            return true;
        }
    }
}

using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using DialogueCreationKit.Dialogue.Models.Enums;
using Newtonsoft.Json;

namespace DialogueCreationKit.Dialogue.Models.Diagram
{
    public class DialoguePortModel : PortModel
    {
        public DialoguePortModel(NodeModel parent, DialogueMessageView dialogueMessage, PortAlignment alignment = PortAlignment.Bottom)
            : base(parent, alignment, null, null)
        {
            DialogueMessage = dialogueMessage;
        }

        [JsonIgnore]
        public DialogueMessageView DialogueMessage { get; }

        public override bool CanAttachTo(PortModel port)
        {
            // Avoid attaching to self port/node
            if (!base.CanAttachTo(port))
                return false;

            var targetPort = port as DialoguePortModel;
            var targetDialogueMessage = targetPort.DialogueMessage;

            if (targetDialogueMessage.Stage == DialogueStage.Begin)
                return false;

            if (!targetDialogueMessage.Id.HasValue )
                return false;

            if (DialogueMessage.Id.Equals(targetDialogueMessage.Id))
                return false;

            //if (DialogueMessage.Primary && targetPort.Links.Count > 0 ||
            //    targetDialogueMessage.Primary && Links.Count > 1) // Ongoing link
            //    return false;

            return true;
        }
    }
}

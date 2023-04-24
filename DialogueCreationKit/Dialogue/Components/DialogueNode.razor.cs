using DialogueCreationKit.Dialogue.Models.Diagram;
using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.Dialogue.Components
{
    public partial class DialogueNode
    {
        [Parameter]
        public DialogueNodeModel Node { get; set; }
    }
}

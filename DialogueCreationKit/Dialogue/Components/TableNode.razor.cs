using DialogueCreationKit.Designer.Models;
using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.Dialogue.Components
{
    public partial class TableNode
    {
        [Parameter]
        public Table Node { get; set; }
    }
}

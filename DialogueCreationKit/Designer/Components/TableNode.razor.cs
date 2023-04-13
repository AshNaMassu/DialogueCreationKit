using DialogueCreationKit.Designer.Models;
using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.Designer.Components
{
    public partial class TableNode
    {
        [Parameter]
        public Table Node { get; set; }
    }
}

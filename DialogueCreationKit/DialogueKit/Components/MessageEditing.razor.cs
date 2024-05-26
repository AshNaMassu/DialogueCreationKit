using DialogueCreationKit.DialogueKit.Domain.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.DialogueKit.Components
{
    public partial class MessageEditing
    {
        [Parameter]
        public DialogueMessageCheckView Message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}

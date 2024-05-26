using DialogueCreationKit.DialogueKit.Domain.Model.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.DialogueKit.View.Components
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

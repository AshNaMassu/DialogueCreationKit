using DialogueCreationKit.DialogueKit.Contracts;
using DialogueCreationKit.DialogueKit.Models.View;
using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.DialogueKit.Components
{
	public partial class MessageEditing
	{
		[Parameter]
		public IDialogueMessageCheckView Message { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
		}
	}
}

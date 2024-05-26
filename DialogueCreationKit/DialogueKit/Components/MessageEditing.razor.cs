using DialogueCreationKit.DialogueKit.Contracts;
using DialogueCreationKit.DialogueKit.Models.View;
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

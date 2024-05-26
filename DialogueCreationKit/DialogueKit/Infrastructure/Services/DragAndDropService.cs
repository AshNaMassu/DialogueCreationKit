using DialogueCreationKit.DialogueKit.Domain.Contracts;
using DialogueCreationKit.DialogueKit.Domain.Contracts.Services;
using Microsoft.AspNetCore.Components.Web;

namespace DialogueCreationKit.DialogueKit.Infrastructure.Services
{
    public class DragAndDropService : IDragAndDropService
    {

        private IDialogueCreationService _dialogueCreationService;
        private int? _dragging;

        public DragAndDropService(IDialogueCreationService dialogueCreationService)
        {
            _dialogueCreationService = dialogueCreationService;
        }


        public void OnDrop(IDialogueCreationModel model, DragEventArgs e, int? s, bool isDraggable)
        {
            if (!isDraggable) return;

            if (s.HasValue && _dragging.HasValue)
            {
                var tempDrag = model.ListMessages[_dragging.Value];
                var tempCurrent = model.ListMessages[s.Value];

                model.ListMessages[s.Value] = tempDrag;
                model.ListMessages[_dragging.Value] = tempCurrent;

                _dragging = null;

                model.Content = "- " + string.Join("\n- ", model.ListMessages.Select(x => x.Message));

                _dialogueCreationService.CreateMessageList(model);

                model.OnUpdateAll();
            }
        }

        public void OnDragStart(DragEventArgs e, int? s, bool isDraggable)
        {
            if (!isDraggable) return;

            e.DataTransfer.DropEffect = "move";
            e.DataTransfer.EffectAllowed = "move";
            _dragging = s;
        }
    }
}

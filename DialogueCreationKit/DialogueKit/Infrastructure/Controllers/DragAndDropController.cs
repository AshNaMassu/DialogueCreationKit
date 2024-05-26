using DialogueCreationKit.DialogueKit.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Web;

namespace DialogueCreationKit.DialogueKit.Domain.Contracts.Services
{
    public class DragAndDropController
    {
        private IDragAndDropService _dragAndDropService;

        public DragAndDropController(IDragAndDropService dragAndDropService)
        {
            if (dragAndDropService == null)
            {
                throw new ArgumentNullException(nameof(dragAndDropService));
            }

            _dragAndDropService = dragAndDropService;
        }

        public void OnDragStart(DragEventArgs e, int? s, bool isDraggable)
        {
            _dragAndDropService.OnDragStart(e, s, isDraggable);
        }

        public void OnDrop(IDialogueCreationModel model, DragEventArgs e, int? s, bool isDraggable)
        {
            _dragAndDropService.OnDrop(model, e, s, isDraggable);
        }
    }
}
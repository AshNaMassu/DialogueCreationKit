using Microsoft.AspNetCore.Components.Web;

namespace DialogueCreationKit.DialogueKit.Domain.Contracts.Services
{
    public interface IDragAndDropService
    {
        void OnDragStart(DragEventArgs e, int? s, bool isDraggable);
        void OnDrop(IDialogueCreationModel model, DragEventArgs e, int? s, bool isDraggable);
    }
}
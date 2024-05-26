using DialogueCreationKit.DialogueKit.Contracts;
using Microsoft.AspNetCore.Components.Web;

namespace DialogueCreationKit.DialogueKit.Managers
{
    public static class DragAndDropManager
    {
        private static int? _dragging;

        public static void OnDrop(IDialogueCreationModel model, DragEventArgs e, int? s, bool isDraggable)
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

                DialogueCreationManager.CreateMessageList(model);

                model.OnUpdateAll();
            }
        }

        public static void OnDragStart(DragEventArgs e, int? s, bool isDraggable)
        {
            if (!isDraggable) return;

            e.DataTransfer.DropEffect = "move";
            e.DataTransfer.EffectAllowed = "move";
            _dragging = s;
        }
    }
}

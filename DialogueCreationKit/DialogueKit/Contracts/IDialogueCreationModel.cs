using DialogueCreationKit.DialogueKit.Enums;
using DialogueCreationKit.DialogueKit.Models;
using DialogueCreationKit.DialogueKit.Models.View;

namespace DialogueCreationKit.DialogueKit.Contracts
{
    public interface IDialogueCreationModel
    {
        string ActorName { get; set; }
        Npc Companion { get; set; }
        string Content { get; set; }
        bool IsEmptyListMessages { get; }
        bool IsEmptyListMessagesMorphy { get; }
        List<DialogueMessageView> ListMessages { get; set; }
        List<DialogueMessageCheckView> ListMessagesMorphy { get; set; }
        List<DialogueStageView> ListStage { get; set; }
        DialogueCreateMode Mode { get; set; }

        event Action OnUpdateAllEvent;

        void OnUpdateAll();
    }
}
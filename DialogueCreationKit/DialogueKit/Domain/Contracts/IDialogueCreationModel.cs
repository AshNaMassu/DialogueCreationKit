using DialogueCreationKit.DialogueKit.Domain.Enums;
using DialogueCreationKit.DialogueKit.Domain.Model;
using DialogueCreationKit.DialogueKit.Domain.Model.ViewModel;

namespace DialogueCreationKit.DialogueKit.Domain.Contracts
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
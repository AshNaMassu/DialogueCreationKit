using Microsoft.JSInterop;

namespace DialogueCreationKit.DialogueKit.Domain.Contracts.Services
{
    public class DialogueCreationController
    {
        private IDialogueCreationService _dialogueCreationService;

        public DialogueCreationController(IDialogueCreationService dialogueCreationService)
        {
            if (dialogueCreationService == null)
            {
                throw new ArgumentNullException(nameof(dialogueCreationService));
            }

            _dialogueCreationService = dialogueCreationService;
        }

        public void CreateMessageList(IDialogueCreationModel model)
        {
            _dialogueCreationService.CreateMessageList(model);
        }

        public void CreateMorphyFromMessages(IDialogueCreationModel model)
        {
            _dialogueCreationService.CreateMorphyFromMessages(model);
        }


        public void Serialize(IJSRuntime js, IDialogueCreationModel model)
        {
            _dialogueCreationService.Serialize(js, model);
        }
        public void SerializeMorphy(IJSRuntime js, IDialogueCreationModel model)
        {
            _dialogueCreationService.SerializeMorphy(js, model);
        }
        public void UpdateTheme(IDialogueCreationModel model, bool needUpdate)
        {
            _dialogueCreationService.UpdateTheme(model, needUpdate);
        }
    }
}
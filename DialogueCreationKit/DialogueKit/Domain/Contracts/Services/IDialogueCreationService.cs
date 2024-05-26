using Microsoft.JSInterop;

namespace DialogueCreationKit.DialogueKit.Domain.Contracts.Services
{
    public interface IDialogueCreationService
    {
        void CreateDialogueTreeNode(IDialogueCreationModel model);
        void CreateMessageList(IDialogueCreationModel model);
        void CreateMessagesFromMorphy(IDialogueCreationModel model);
        void CreateMorphyFromMessages(IDialogueCreationModel model);
        void CreateOrUpdateTheme(IDialogueCreationModel model);
        Task DownloadFile(IJSRuntime js, string fileName, string content);
        void Serialize(IJSRuntime js, IDialogueCreationModel model);
        void SerializeMorphy(IJSRuntime js, IDialogueCreationModel model);
        void UpdateTheme(IDialogueCreationModel model, bool needUpdate);
    }
}
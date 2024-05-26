using DialogueCreationKit.DialogueKit.Domain.Model;
using DialogueCreationKit.DialogueKit.Domain.Model.Responses;

namespace DialogueCreationKit.DialogueKit.Domain.Contracts.Services
{
    public interface IMorphemesService
    {
        BaseResponse Status { get; set; }

        void GetMessageWithOptions(List<DialogueMessageCheck> externalValues);
        Task<DialogueMessageCheck> WordsInitializer(string word);
    }
}
using DialogueCreationKit.DialogueKit.Domain.Model;
using DialogueCreationKit.DialogueKit.Domain.Model.Responses;

namespace DialogueCreationKit.DialogueKit.Domain.Contracts.Services
{
    public class MorphemesController
    {
        private IMorphemesService _morphemesService;

        public MorphemesController(IMorphemesService morphemesService)
        {
            if (morphemesService == null)
            {
                throw new ArgumentNullException(nameof(morphemesService));
            }

            _morphemesService = morphemesService;
        }

        public BaseResponse Status 
        { 
            get => _morphemesService.Status; 
            set => _morphemesService.Status = value; 
        }

        public void GetMessageWithOptions(List<DialogueMessageCheck> externalValues)
        {
            _morphemesService.GetMessageWithOptions(externalValues);
        }

        public async Task<DialogueMessageCheck> WordsInitializer(string word)
        {
            return await _morphemesService.WordsInitializer(word);
        }
    }
}
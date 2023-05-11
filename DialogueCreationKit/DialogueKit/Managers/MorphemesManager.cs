using System.Text.RegularExpressions;
using DialogueCreationKit.DialogueKit.Managers.Responses;
using DialogueCreationKit.DialogueKit.Helpers;

namespace DialogueCreationKit.DialogueKit.Managers;

public static class MorphemesManager
{
    private static string[] words;

    /// <summary>
    /// Инициализирует 
    /// </summary>
    /// <param name="phrase"> Входные данные. Текст на русском языке</param>
    public static BaseResponse WordsInitializer(string phrase)
    {
        if (phrase == String.Empty)
            return new BaseResponse()
                { Code = 1001, Message = @"Входная строка пустая!", Status = false };

        if (!phrase.IsCyrillicText())
            return new BaseResponse()
                {Code = 1003, Message = @"Текст или его часть не отвечает требованиям языка.(Русский)", Status = false};

     
        return new BaseResponse();
    }

   
}
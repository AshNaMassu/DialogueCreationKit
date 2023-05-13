using System.Text.RegularExpressions;
using DialogueCreationKit.DialogueKit.Managers.Responses;
using DialogueCreationKit.DialogueKit.Helpers;
using DeepMorphy;
using DeepMorphy.Model;

namespace DialogueCreationKit.DialogueKit.Managers;

public static class MorphemesManager
{
    private static string[] words;
    private static string originalSentence;
    private static List<MorphInfo> morphs = new List<MorphInfo>();
    private static MorphAnalyzer _morphAnalyzer = new MorphAnalyzer();

    public static string OriginalSentence => originalSentence;
    public static List<MorphInfo> Words => morphs;

    /// <summary>
    /// Инициализирует 
    /// </summary>
    /// <param name="phrase"> Входные данные. Текст на русском языке</param>
    public static BaseResponse WordsInitializer(string phrase)
    {
        if (phrase == string.Empty)
            return new BaseResponse()
            { Code = 1001, Message = @"Входная строка пустая!", Status = false };

        if (!phrase.IsCyrillicText())
            return new BaseResponse()
            { Code = 1003, Message = @"Текст или его часть не отвечает требованиям языка.(Русский)", Status = false };

        originalSentence = phrase;
        words = phrase.RemoveExtraSpaces().Split(' ');
        var VerbMorfs = new List<MorphInfo>();
        var results = _morphAnalyzer.Parse(words).ToList();
        foreach (var morf in results)
        {
            morf.BestTag.Has("гл");
            if (morf.BestTag.Has("гл") || morf.BestTag.Has("инф_гл"))
                morphs.Add(morf);
        }
        return new BaseResponse()
        { Code = 0, Message = @"Инициализация успешна!", Status = true };
    }

    public static string GetMessageWithOptions(bool fairytaleFlag = false)
    {
        int tagId = 0;
        string type = "";
        foreach (var morph in morphs)
        {
            if (morph.BestTag.ToString().Split(',').First() == "гл") type = "инф_гл"; // лажа
            else type = "гл";
            tagId++;
            var tasks = new[]
            {
                new InflectTask(morph.Text,
                    _morphAnalyzer.TagHelper.CreateTag(morph.BestTag.ToString().Split(',').First()),
                    _morphAnalyzer.TagHelper.CreateTag(type, nmbr: "ед", tens: "буд", pers: "1л", mood: "изъяв")),
                //гл,мн,прош,изъяв
                new InflectTask(morph.Text,
                    _morphAnalyzer.TagHelper.CreateTag(morph.BestTag.ToString().Split(',').First()),
                    _morphAnalyzer.TagHelper.CreateTag(type, nmbr: "мн", tens: "прош",  mood: "изъяв")),
                //гл,муж,ед,прош,изъяв
                new InflectTask(morph.Text,
                    _morphAnalyzer.TagHelper.CreateTag(morph.BestTag.ToString().Split(',').First()),
                    _morphAnalyzer.TagHelper.CreateTag(type, gndr: "муж", nmbr: "ед", tens: "прош",  mood: "изъяв")),


            };
            //Console.WriteLine(_morphAnalyzer.Inflect(tasks).First());
            var results = _morphAnalyzer.Inflect(tasks);
            foreach (var result in results)
            {
                Console.WriteLine(result);
                Console.WriteLine("======================");
            }
        }

        return "  ";
    }


}
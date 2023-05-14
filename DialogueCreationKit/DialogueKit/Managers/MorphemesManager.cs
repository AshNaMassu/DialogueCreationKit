using System.Text.RegularExpressions;
using DialogueCreationKit.DialogueKit.Managers.Responses;
using DialogueCreationKit.DialogueKit.Helpers;
using DeepMorphy;
using DeepMorphy.Model;
using Microsoft.AspNetCore.Routing.Constraints;
using AntDesign;
using System.Threading.Tasks;
using DialogueCreationKit.DialogueKit.Models;
using System.Collections.Generic;

namespace DialogueCreationKit.DialogueKit.Managers;

public class MorphInfoInfinitive
{
    public string Value { get; set;}
    public bool IsInfinitive { get; set;}
}

public static class MorphemesManager
{
    private static string[] words;
    private static string originalSentence;
    private static List<MorphInfoInfinitive> morphs = new List<MorphInfoInfinitive>();
    private static MorphAnalyzer _morphAnalyzer = new MorphAnalyzer();

    public static string OriginalSentence => originalSentence;
    public static List<MorphInfoInfinitive> Words => morphs;

    private static List<Func<string, string, string, InflectTask?>> _funcsCreateTags = new() { CreateTask0, CreateTask1, CreateTask2 };

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

        morphs = new List<MorphInfoInfinitive>();
        originalSentence = phrase;
        words = phrase.RemoveExtraSpaces().Split(' ');
        var VerbMorfs = new List<MorphInfo>();
        var results = _morphAnalyzer.Parse(words).ToList();
        foreach (var morf in results)
        {
            //morf.BestTag.Has("гл");
            if (morf.BestTag.Has("гл") || morf.BestTag.Has("инф_гл"))
            {
                morphs.Add( new MorphInfoInfinitive() { Value = morf.Text, IsInfinitive = morf.BestTag.Has("инф_гл") });
            }
        }

        return new BaseResponse()
        { Code = 0, Message = @"Инициализация успешна!", Status = true };
    }

    public static void GetMessageWithOptions(List<DialogueMessageCheck> checks)
    {
        foreach (var check in checks)
        {
            var text = check.Value;
            var srcTag = check.IsInfinitive ? "инф_гл" : "гл";
            var type = !check.IsInfinitive ? "инф_гл" : "гл";

            check.Value = text;

            if (!srcTag.Equals("инф_гл"))
            {
                var task = CreateTaskInfinitive(text, srcTag, type);

                if (task != null && task.HasValue)
                {
                    var infinitive = _morphAnalyzer.Inflect(new List<InflectTask> { task.Value });
                    if (infinitive != null && infinitive.Count() != 0)
                    {
                        check.Infinitive = infinitive.FirstOrDefault();
                    }
                }
            }
            else
            {
                check.IsInfinitive = true;
                check.Infinitive = text;
            }

            var tasks = CreateTasks(text, srcTag, type);

            if (tasks != null)
            {
                var variants = _morphAnalyzer.Inflect(tasks);

                if (variants != null && variants.Count() != 0)
                {
                    check.Variants = variants.Select( x => new Variant(x)).ToList();
                }
            }
        }
    }

    private static List<InflectTask> CreateTasks(string morph, string srcTag, string type)
    {
        List<InflectTask> tasks = new();

        try
        {
            foreach (var func in _funcsCreateTags)
            {
                var task = func(morph, srcTag, type);

                if (task != null && task.HasValue)
                    tasks.Add(task.Value);
            }

            return (tasks.Count == 0) ? null : tasks;
        }
        catch (Exception ex)
        {
            return (tasks.Count == 0) ? null : tasks;
        }
    }

    private static InflectTask? CreateTaskInfinitive(string morph, string srcTag, string type)
    {
        try
        {
            return new InflectTask(morph,
                    _morphAnalyzer.TagHelper.CreateTag(srcTag),
                    _morphAnalyzer.TagHelper.CreateTag(type));
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private static InflectTask? CreateTask0(string morph, string srcTag, string type)
    {
        try
        {
            return new InflectTask(morph,
                 _morphAnalyzer.TagHelper.CreateTag(srcTag),
                 _morphAnalyzer.TagHelper.CreateTag(type, nmbr: "ед", tens: "буд", pers: "1л", mood: "изъяв"));
        }
        catch( Exception ex) 
        {
            return null ;
        }
    }

    private static InflectTask? CreateTask1(string morph, string srcTag, string type)
    {
        try
        {
            return new InflectTask(morph,
                    _morphAnalyzer.TagHelper.CreateTag(srcTag),
                    _morphAnalyzer.TagHelper.CreateTag(type, nmbr: "мн", tens: "прош", mood: "изъяв"));
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private static InflectTask? CreateTask2(string morph, string srcTag, string type)
    {
        try
        {
            return new InflectTask(morph,
                    _morphAnalyzer.TagHelper.CreateTag(srcTag),
                    _morphAnalyzer.TagHelper.CreateTag(type, gndr: "муж", nmbr: "ед", tens: "прош", mood: "изъяв"));
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
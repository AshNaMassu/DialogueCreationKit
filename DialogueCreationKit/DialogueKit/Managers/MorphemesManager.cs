using System.Text.RegularExpressions;
using DialogueCreationKit.DialogueKit.Helpers;
using DeepMorphy;
using DeepMorphy.Model;
using Microsoft.AspNetCore.Routing.Constraints;
using AntDesign;
using System.Threading.Tasks;
using DialogueCreationKit.DialogueKit.Models;
using System.Collections.Generic;
using DialogueCreationKit.DialogueKit.Models.Responses;

namespace DialogueCreationKit.DialogueKit.Managers;

public static class MorphemesManager
{
    private static MorphAnalyzer _morphAnalyzer { get; set; } = new MorphAnalyzer(true);

    private static List<Func<string, string, InflectTask?>> _funcsCreateTags = new() { CreateTask0, CreateTask1, CreateTask2 };

    public static BaseResponse Status { get; set; }    

    /// <summary>
    /// Инициализирует 
    /// </summary>
    /// <param name="word"> Входные данные. Текст на русском языке</param>
    public static async Task<DialogueMessageCheck> WordsInitializer(string word)
    {
        

        if (string.IsNullOrWhiteSpace(word))
        {
            Status = new BaseResponse()
            { Code = 1001, Message = @"Входная строка пустая!", Status = false };

            return null;
        }

        if (!word.IsCyrillicText())
        {
            Status = new BaseResponse()
            { Code = 1003, Message = @"Текст или его часть не отвечает требованиям языка.(Русский)", Status = false };
            return null;
        }

        DialogueMessageCheck externalValue = null;

        var results = await Task.Run(() => _morphAnalyzer.Parse(word).ToList());

        foreach (var morph in results)
        {
            if (morph.BestTag.Has("гл") || morph.BestTag.Has("инф_гл"))
            {
                var lemma = morph.BestTag.Lemma;
                externalValue = new DialogueMessageCheck() { Value = morph.Text, Infinitive = lemma };
            }
        }

        Status = new BaseResponse()
        { Code = 0, Message = @"Инициализация успешна!", Status = true };

        return externalValue;
    }

    public static void GetMessageWithOptions(List<DialogueMessageCheck> externalValues)
    {
        foreach (var check in externalValues)
        {
            if (string.IsNullOrWhiteSpace(check.Infinitive)) continue;

            var tasks = CreateTasks(check.Infinitive, "гл");

            if (tasks != null)
            {
                var variants = _morphAnalyzer.Inflect(tasks);

                if (variants != null && variants.Count() != 0)
                {
                    check.VariantsValue = variants.Select( x => new Variant(x)).ToList();
                }
            }
        }
    }

    private static List<InflectTask> CreateTasks(string morph, string type)
    {
        List<InflectTask> tasks = new();

        try
        {
            foreach (var func in _funcsCreateTags)
            {
                var task = func(morph, type);

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

    private static InflectTask? CreateTask0(string morph, string type)
    {
        try
        {
            return new InflectTask(morph,
                 _morphAnalyzer.TagHelper.CreateTag("инф_гл"),
                 _morphAnalyzer.TagHelper.CreateTag(type, nmbr: "ед", tens: "буд", pers: "1л", mood: "изъяв"));
        }
        catch( Exception ex) 
        {
            return null ;
        }
    }

    private static InflectTask? CreateTask1(string morph, string type)
    {
        try
        {
            return new InflectTask(morph,
                    _morphAnalyzer.TagHelper.CreateTag("инф_гл"),
                    _morphAnalyzer.TagHelper.CreateTag(type, nmbr: "мн", tens: "прош", mood: "изъяв"));
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private static InflectTask? CreateTask2(string morph, string type)
    {
        try
        {
            return new InflectTask(morph,
                    _morphAnalyzer.TagHelper.CreateTag("инф_гл"),
                    _morphAnalyzer.TagHelper.CreateTag(type, gndr: "муж", nmbr: "ед", tens: "прош", mood: "изъяв"));
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
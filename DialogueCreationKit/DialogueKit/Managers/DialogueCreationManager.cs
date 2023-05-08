    using DialogueCreationKit.DialogueKit.Enums;
using DialogueCreationKit.DialogueKit.Models;
using DialogueCreationKit.DialogueKit.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Immutable;
using System.Text;
using System.Xml.Linq;

namespace DialogueCreationKit.DialogueKit.Managers
{
    public static class DialogueCreationManager
    {
        public static void CreateMessageList(DialogueCreationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(model.Content)) return;// throw new ArgumentException(nameof(model));

            model.Content = model.Content.Replace("\r", "");
            var messages = model.Content.Split('\n');

            if (model.ListMessages == null)
                model.ListMessages = new List<DialogueMessageView>();

            var startBoundIndex = model.ListMessages.Count;

            for ( int indexMessage = 0; indexMessage < messages.Length; indexMessage ++ )
            {
                int i = 0;
                for (; i < messages[indexMessage].Length; i++)
                {
                    if (char.IsLetter(messages[indexMessage][i]))
                        break;
                }

                if (indexMessage < startBoundIndex)
                    model.ListMessages[indexMessage] = new(indexMessage, messages[indexMessage].Substring(i, messages[indexMessage].Length - i));
                else
                    model.ListMessages.Add(new(indexMessage, messages[indexMessage].Substring(i, messages[indexMessage].Length - i)));
            }

            if (model.ListMessages.Count > messages.Length)
                model.ListMessages.RemoveAt(messages.Length);

            CreateOrUpdateTheme(model);
        }

        public static void CreateOrUpdateTheme(DialogueCreationModel model)
        {
            if (model.ListMessages == null || model.ListMessages.Count == 0) throw new ArgumentException(nameof(model.ListMessages));

            bool needUpdate = true;

            if (model.ListStage == null)
            {
                model.ListStage = new List<DialogueStageView>();
                model.ListStage = model.ListMessages.Select( x => new DialogueStageView()).ToList();
            }
            else
            {
                if (model.ListMessages.Count > model.ListStage.Count)
                {
                    needUpdate = true;
                    int dif = model.ListMessages.Count - model.ListStage.Count;

                    for (var i = 0; i < dif; i++)
                    {
                        model.ListStage.Add(new DialogueStageView());
                    }
                }
                else if (model.ListMessages.Count < model.ListStage.Count)
                {
                    model.ListStage.RemoveAt(model.ListMessages.Count);
                }
                else
                {
                    needUpdate = false;
                }
            }

            model.ListStage[2].IsNewTheme = true;

            for (int i = 0; i < model.ListStage.Count; i++)
            {
                var stage = model.ListStage[i];

                if (i < 2)
                {
                    stage.Stage = DialogueStage.Begin;
                }
                else if (i > model.ListStage.Count - 3 + model.ListStage.Count % 2)
                {
                    stage.Stage = DialogueStage.End;
                }
                else
                {
                    stage.Stage = DialogueStage.Content;
                }
            }

            UpdateTheme(model, needUpdate);
        }

        public static void UpdateTheme(DialogueCreationModel model, bool needUpdate)
        {
            if (!needUpdate) return;

            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.ListMessages == null || model.ListMessages.Count == 0) throw new ArgumentException(nameof(model.ListMessages));
            //if (model.Actor == null) throw new ArgumentException(nameof(model.Actor));
            if (model.Companion == null) throw new ArgumentException(nameof(model.Companion));
            
            int t = 0;
            foreach (var stage in model.ListStage)
            {
                if (stage.IsNewTheme)
                    t = (t + 1) % 2;

                stage.IdTheme = t;
            }

            model.OnUpdateAll();
        }

        public static void CreateDialogueTreeNode(DialogueCreationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.ListMessages == null || model.ListMessages.Count == 0) throw new ArgumentException(nameof(model.ListMessages));
            //if (model.Actor == null) throw new ArgumentException(nameof(model.Actor));
            if (model.Companion == null) throw new ArgumentException(nameof(model.Companion));

        }

        public static async void Serialize(IJSRuntime js, DialogueCreationModel model)
        {
            if (model.ListMessages == null || model.ListMessages.Count == 0) return; 

            if (model.Mode == DialogueCreateMode.RandomMessages)
            {
                DialogueRandom random = new DialogueRandom();

                model.ListMessages.ForEach(x => random.Content.Add(x.Message));

                var json = JsonConvert.SerializeObject(random);

                await DownloadFile(js, model.ActorName + "_" + random.Id.ToString() + ".random_json", json);
            }
            else
            {
                if (model.ListMessages.Count != model.ListStage.Count) return;

                DialogueTree tree = new DialogueTree();
                tree.Npc = model.Companion;

                var messages = model.ListMessages;
                var stages = model.ListStage;
                List<DialogueNode> nodes = new List<DialogueNode>();

                messages.ForEach(x => nodes.Add( new DialogueNode() { Message = x.Message, Stage = stages[x.Id].Stage }));

                nodes[0].Child = nodes[1].Id;

                for (int i = 2; i < nodes.Count; i++)
                { 
                    switch (nodes[i].Stage)
                    {
                        case DialogueStage.Content:
                            {
                                if ((i % 2 == 0) || (stages[i + 1].Stage != DialogueStage.End && stages[i + 1].IsNewTheme))
                                {
                                    nodes[i].Child = nodes[i + 1].Id;
                                }
                            }
                            break;

                        case DialogueStage.End:
                            {
                                if ((nodes.Count % 2 == 0) && (i == nodes.Count - 2))
                                    nodes[i].Child = nodes[i + 1].Id;
                            }
                            break;
                    }
                }

                tree.Begin = nodes.FirstOrDefault(x => x.Stage == DialogueStage.Begin);

                //tree.Content = nodes.FirstOrDefault(x => x.Stage == DialogueStage.Content && x.Child.);
                for (int i = 2; i < stages.Count - 3 + stages.Count % 2; i+= 2)
                {
                    if (stages[i].IsNewTheme)
                        tree.Content.Add(nodes[i]);
                }

                tree.End = nodes.FirstOrDefault(x => x.Stage == DialogueStage.End);

                var jsonNodes = JsonConvert.SerializeObject(nodes);
                await DownloadFile(js, tree.Npc.Name + "_" + tree.Id.ToString() + ".nodes_json", jsonNodes);

                var jsonTree = JsonConvert.SerializeObject(tree);
                await DownloadFile(js, tree.Npc.Name + "_" + tree.Id.ToString() + ".tree_json", jsonTree);
            }
        }

        public static async Task DownloadFile(IJSRuntime js, string fileName, string content)
        {
            // Конвертация строки JSON в байты
            byte[] byteArray = Encoding.UTF8.GetBytes(content);

            // Отправка файла клиенту
            await js.InvokeVoidAsync("downloadFile", $"{fileName}", byteArray);
        }
    }
}

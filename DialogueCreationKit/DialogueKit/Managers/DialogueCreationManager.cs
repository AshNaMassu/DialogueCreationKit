using DialogueCreationKit.DialogueKit.Enums;
using DialogueCreationKit.DialogueKit.Models.View;
using Microsoft.JSInterop;
using Newtonsoft.Json;
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
            if (model.Actor == null) throw new ArgumentException(nameof(model.Actor));
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
            if (model.Actor == null) throw new ArgumentException(nameof(model.Actor));
            if (model.Companion == null) throw new ArgumentException(nameof(model.Companion));

        }

        public static async Task DownloadFile(IJSRuntime js)
        {
            var data = new object();
            // Сериализация данных в формат JSON
            string json = JsonConvert.SerializeObject(data);

            // Конвертация строки JSON в байты
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            // Отправка файла клиенту
            await js.InvokeVoidAsync("downloadFile", "file.json", byteArray);
        }
    }
}

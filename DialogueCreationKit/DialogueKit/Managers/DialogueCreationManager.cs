using DialogueCreationKit.DialogueKit.Enums;
using DialogueCreationKit.DialogueKit.Models;

namespace DialogueCreationKit.DialogueKit.Managers
{
    public static class DialogueCreationManager
    {
        public static void CreateMessageList(DialogueCreationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrWhiteSpace(model.Content)) throw new ArgumentException(nameof(model));

            var messages = model.Content.Split('\n');

            if (model.ListMessages == null)
                model.ListMessages = new List<DialogueMessageView>();

            var startCount = model.ListMessages.Count;

            for ( int indexMessage = 0; indexMessage < messages.Length; indexMessage ++ )
            {
                int i = 0;
                for (; i < messages[indexMessage].Length; i++)
                {
                    if (char.IsLetter(messages[indexMessage][i]))
                        break;
                }

                if (i < startCount)
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

            if (model.ListStage == null)
            {
                model.ListStage = new List<DialogueStageView>();
                model.ListStage = model.ListMessages.Select( x => new DialogueStageView()).ToList();
            }
            else
            {
                if (model.ListMessages.Count > model.ListStage.Count)
                {
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
            }

            for (int i = 0; i < model.ListStage.Count; i++)
            {
                if (i < 2)
                {
                    model.ListStage[i].Stage = DialogueStage.Begin;
                }
                else if (i > model.ListStage.Count - 3 + model.ListStage.Count % 2)
                {
                    model.ListStage[i].Stage = DialogueStage.End;
                }
                else
                {
                
                }
            }
        }

        public static void UpdateTheme(DialogueCreationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.ListMessages == null || model.ListMessages.Count == 0) throw new ArgumentException(nameof(model.ListMessages));
            if (model.Actor == null) throw new ArgumentException(nameof(model.Actor));
            if (model.Companion == null) throw new ArgumentException(nameof(model.Companion));
            /*
            var content = model.ListMessages.Where(x => x.Node.Stage == DialogueStage.Content);

            int t = 0;
            foreach (var node in content)
            {
                if (!node.Node.Child.HasValue)
                    t++;

                node.Theme = t;
            }
            */
        }

        public static void CreateDialogueTreeNode(DialogueCreationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.ListMessages == null || model.ListMessages.Count == 0) throw new ArgumentException(nameof(model.ListMessages));
            if (model.Actor == null) throw new ArgumentException(nameof(model.Actor));
            if (model.Companion == null) throw new ArgumentException(nameof(model.Companion));

            /*
            DialogueNode nodeLast = null;

            for (int i = 0; i < model.ListMessages.Count; i++)
            {
                var message = model.ListMessages[i];
                if (i > 0)
                    nodeLast = model.ListMessages[i - 1].Node;
                
                message.Message.Id = Guid.NewGuid();
                
                
                message.Npc = i % 2 == 0 ? model.Actor : model.Companion;

                message.Node.Id = Guid.NewGuid();
                message.Node.MessageId = message.Message.Id;

                if (i < 2)
                {
                    message.Node.Stage = DialogueStage.Begin;

                    if (i == 1)
                        nodeLast.Child = message.Node.Id;
                }
                else if (i > model.ListMessages.Count - 3)
                {
                    message.Node.Stage = DialogueStage.End;

                    if (i == model.ListMessages.Count - 1)
                        message.Node.Child = message.Node.Id;
                }
                else
                {
                    message.Node.Stage = DialogueStage.Content;

                    if (i > 3)
                        message.Node.Child = nodeLast.Id;
                } 
            }    
            */
        }
    }
}

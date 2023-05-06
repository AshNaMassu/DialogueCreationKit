using DialogueCreationKit.DialogueKit.Enums;
using DialogueCreationKit.DialogueKit.Models;

namespace DialogueCreationKit.DialogueKit.Managers
{
    public static class CreateDialogueManager
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
                    model.ListMessages[indexMessage] = new(messages[indexMessage].Substring(i, messages[indexMessage].Length - i));
                else
                    model.ListMessages.Add(new(messages[indexMessage].Substring(i, messages[indexMessage].Length - i)));
            }

            if (model.ListMessages.Count > messages.Length)
                model.ListMessages.RemoveAt(messages.Length);

            CreateDialogueTreeNode(model);
        }

        public static void CreateDialogueTreeNode(DialogueCreationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.ListMessages == null || model.ListMessages.Count == 0) throw new ArgumentException(nameof(model.ListMessages)); 
            if (model.ListNpc == null || model.ListNpc.Count == 0) throw new ArgumentException(nameof(model.ListNpc));

            Npc first = model.ListNpc[model.ListNpc[0].IsFisrt ? 0 : 1],
            second = model.ListNpc[model.ListNpc[0].IsFisrt ? 1 : 0];

            for (int i = 0; i < model.ListMessages.Count; i++)
            {
                var message = model.ListMessages[i];
                message.Message.Id = Guid.NewGuid();
                message.Npc = i % 2 == 0 ? first : second;

                message.Node.Id = Guid.NewGuid();
                message.Node.MessageId = message.Message.Id;

                if (i < 2)
                {
                    message.Node.Stage = DialogueStage.Begin;
                }
                else if (i > model.ListMessages.Count - 3)
                {
                    message.Node.Stage = DialogueStage.End;
                }
                else
                {
                    message.Node.Stage = DialogueStage.Content;
                }
            }    
        }
    }
}

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

        public static void UpdateTheme(DialogueCreationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.ListMessages == null || model.ListMessages.Count == 0) throw new ArgumentException(nameof(model.ListMessages));
            if (model.ListNpc == null || model.ListNpc.Count == 0) throw new ArgumentException(nameof(model.ListNpc));

            var content = model.ListMessages.Where(x => x.Node.Stage == DialogueStage.Content);

            int t = 0;
            foreach (var node in content)
            {
                node.Theme = t;
                if (node.Node.Childs.Count == 1)
                    t++;
            }
        }

        public static void CreateDialogueTreeNode(DialogueCreationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (model.ListMessages == null || model.ListMessages.Count == 0) throw new ArgumentException(nameof(model.ListMessages)); 
            if (model.ListNpc == null || model.ListNpc.Count == 0) throw new ArgumentException(nameof(model.ListNpc));

            Npc first = model.ListNpc[model.ListNpc[0].IsFisrt ? 0 : 1],
            second = model.ListNpc[model.ListNpc[0].IsFisrt ? 1 : 0];

            DialogueNode nodeLast = null;

            for (int i = 0; i < model.ListMessages.Count; i++)
            {
                var message = model.ListMessages[i];
                if (i > 0)
                    nodeLast = model.ListMessages[i - 1].Node;
                
                message.Message.Id = Guid.NewGuid();
                message.Npc = i % 2 == 0 ? first : second;

                message.Node.Id = Guid.NewGuid();
                message.Node.MessageId = message.Message.Id;
                message.Node.Childs.Add(Guid.Empty);

                if (i < 2)
                {
                    message.Node.Stage = DialogueStage.Begin;

                    if (i == 1)
                        nodeLast.Childs.Add(message.Node.Id);
                }
                else if (i > model.ListMessages.Count - 3)
                {
                    message.Node.Stage = DialogueStage.End;

                    if (i == model.ListMessages.Count - 1)
                        message.Node.Childs.Add(message.Node.Id);
                }
                else
                {
                    message.Node.Stage = DialogueStage.Content;

                    message.Node.Childs.Add(new Guid());

                    if (i > 3)
                        message.Node.Childs.Add(nodeLast.Id);
                }
            }    
        }
    }
}

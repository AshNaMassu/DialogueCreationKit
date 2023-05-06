using DialogueCreationKit.DialogueKit.Models;

namespace DialogueCreationKit.DialogueKit.Managers
{
    public static class CreateDialogueManager
    {
        public static void CreateMessageList(DialogueCreationModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var messages = model.Content.Split(Environment.NewLine);

            model.ListMessages = new List<string>();

            for ( int indexMessage = 0; indexMessage < messages.Length; indexMessage ++ )
            {
                int i = 0;
                for (; i < messages[indexMessage].Length; i++)
                {
                    if (char.IsLetter(messages[indexMessage][i]))
                        break;
                }

                model.ListMessages.Add(messages[indexMessage].Substring(i, messages[indexMessage].Length - i));
            }
        }

        public static void CreateDialogueTreeNode(DialogueCreationModel model)
        { 

        }
    }
}

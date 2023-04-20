
namespace DialogueCreationKit.Dialogue.Models
{
    [Serializable]
    public enum DialogueState
    {
        Begin,
        Content,
        End
    }

    [Serializable]
    public class DialogueMessage
    {
        public string IdMessage;
        public bool IsActive;
        public DialogueState State;
        public string Message;
        //public string[] IdChild;
        public DialogueAnswer DialogueAnswer;

        public event Action Changed;
        public void Refresh() => Changed?.Invoke();
    }

    [Serializable]
    public class DialogueAnswer
    {
        public string AnswerCorrect;
        public string AnswerWrong;

        public string GetAnswer(bool isCorrect)
        {
            return isCorrect ? AnswerCorrect : AnswerWrong;
        }
    }


    [Serializable]
    public class DialogueNode
    {
        public string IdDialogueMessage;
        public List<DialogueNode> ChildNodes;

        public DialogueNode()
        {
            IdDialogueMessage = String.Empty;
            ChildNodes = new();
        }
    }

    [Serializable]
    public class DialogTree
    {
        public string NPC;
        public DialogueNode Root;
    }
}

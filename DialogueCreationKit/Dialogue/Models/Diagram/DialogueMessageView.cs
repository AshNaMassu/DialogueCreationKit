using Blazor.Diagrams.Core.Models;
using DialogueCreationKit.Dialogue.Models.Dialogue;
using DialogueCreationKit.Dialogue.Models.Enums;

namespace DialogueCreationKit.Dialogue.Models.Diagram
{
    public class DialogueMessageView : DialogueMessage
    {
        public event Action Changed;
        public List<DialogueMessageView> ChildsNodes { get; set; }

        public void Refresh() => Changed?.Invoke();

        public DialogueMessageView() : base()
        {
            ChildsNodes = new List<DialogueMessageView>();
        }

        public DialogueMessageView(int countOfChild) : base()
        {
            ChildsNodes = new List<DialogueMessageView>();

            for (int i = 0; i < countOfChild; i++)
            {
                ChildsNodes.Add(new DialogueMessageView());
            }   
        }
    }   
}

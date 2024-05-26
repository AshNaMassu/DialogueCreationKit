using DialogueCreationKit.DialogueKit.Domain.Contracts;
using DialogueCreationKit.DialogueKit.Domain.Enums;

namespace DialogueCreationKit.DialogueKit.Domain.Model.ViewModel
{
    public class DialogueCreationModel : IDialogueCreationModel
    {
        //public Npc Actor { get; set; }
        public string ActorName { get; set; }
        public Npc Companion { get; set; }

        public string Content { get; set; }
        public DialogueCreateMode Mode { get; set; }

        public bool IsEmptyListMessages => ListMessages == null || ListMessages.Count == 0;
        public bool IsEmptyListMessagesMorphy => ListMessagesMorphy == null || ListMessagesMorphy.Count == 0;

        public List<DialogueMessageView> ListMessages { get; set; }
        public List<DialogueMessageCheckView> ListMessagesMorphy { get; set; }
        public List<DialogueStageView> ListStage { get; set; }

        public event Action OnUpdateAllEvent;

        public void OnUpdateAll()
        {
            if (OnUpdateAllEvent != null)
                OnUpdateAllEvent.Invoke();
        }

        public DialogueCreationModel()
        {
            //Actor = new Npc();
            ActorName = new List<string>() { "Довакин", "Слышащий", "Соловей", "Архимаг", "Клинок", "Пенитус Окулатус" }[new Random().Next(0, 6)];
            Companion = new Npc();
            Content = string.Empty;
            Mode = DialogueCreateMode.Dialogue;
            ListMessages = new List<DialogueMessageView>();
            ListMessagesMorphy = new List<DialogueMessageCheckView>();
            ListStage = new List<DialogueStageView>();
        }
    }
}

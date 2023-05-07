using DialogueCreationKit.DialogueKit.Enums;

namespace DialogueCreationKit.DialogueKit.Models
{
    public class DialogueStageView
    {
        public int Id { get; set; }
        public DialogueStage Stage { get; set; }

        public int Theme { get; set; }

        public DialogueStageView()
        {
            Stage = DialogueStage.Content;
        }
    }
}

using DialogueCreationKit.DialogueKit.Enums;

namespace DialogueCreationKit.DialogueKit.Models
{
    public class DialogueStageView
    {
        public DialogueStage Stage { get; set; }

        public bool IsNewTheme 
        { 
            get => _isNewTheme; 
            set
            {
                if (_isNewTheme != value) 
                {
                    _isNewTheme = value;
                    if (OnUpdateThemeEvent != null)
                        OnUpdateThemeEvent.Invoke();
                }
            } 
        }
        private bool _isNewTheme;
        public int IdTheme { get; set; }

        public event Action OnUpdateThemeEvent;

        public DialogueStageView()
        {
            Stage = DialogueStage.Content;
        }
    }
}

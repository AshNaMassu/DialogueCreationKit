﻿using DialogueCreationKit.DialogueKit.Enums;

namespace DialogueCreationKit.DialogueKit.Models.View
{
    public class DialogueCreationModel
    {
        //public Npc Actor { get; set; }
        public string ActorName { get; set; }
        public Npc Companion { get; set; }

        public string Content { get; set; }
        public DialogueCreateMode Mode { get; set; }

        public List<DialogueMessageView> ListMessages { get; set; }
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
            ActorName = new List<string>() { "Довакин", "Слышащий", "Соловей", "Архимаг", "Клинок", "Пенитус Окулатус", "ГЕЙмерская Чепушила Ушастая" }[new Random().Next(0, 7)];
            Companion = new Npc();
            Content = string.Empty;
            Mode = DialogueCreateMode.Dialogue;
            ListMessages = new List<DialogueMessageView>();
            ListStage = new List<DialogueStageView>();
        }
    }
}

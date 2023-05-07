﻿namespace DialogueCreationKit.DialogueKit.Models
{

    [Serializable]
    public class DialogueMessage
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public DialogueMessage() 
        {
            Content = string.Empty;
        }

    }
}
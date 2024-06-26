﻿using Microsoft.AspNetCore.Identity;

namespace DialogueCreationKit.DialogueKit.Domain.Model
{
    public class DialogueMessageCheck
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Infinitive { get; set; }

        [NonSerialized]
        public List<Variant> VariantsValue;

        public List<string> Variants { get; set; }

        public DialogueMessageCheck()
        {
            Id = Guid.NewGuid();
            Value = string.Empty;
            Infinitive = string.Empty;
            VariantsValue = new();
        }
    }

    public class Variant
    {
        public string Value { get; set; }

        public Variant(string v)
        {
            Value = v;
        }
    }
}

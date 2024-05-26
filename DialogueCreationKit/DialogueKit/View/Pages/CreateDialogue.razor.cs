using DialogueCreationKit.DialogueKit.Domain.Contracts;
using DialogueCreationKit.DialogueKit.Domain.Contracts.Services;
using DialogueCreationKit.DialogueKit.Domain.Enums;
using DialogueCreationKit.DialogueKit.Domain.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DialogueCreationKit.DialogueKit.View.Pages
{
    public partial class CreateDialogue : IDisposable
    {
        [Inject] IJSRuntime _jsRuntime { get; set; }
        [Inject] IDialogueCreationModel _model { get; set; }
        [Inject] DialogueCreationController _dialogueCreationController { get; set; }

        public string _content { get; set; }
        public List<Npc> Scene0 { get; set; }
        public List<Npc> Scene1 { get; set; }

        private SceneEnum _scene { get; set; } = SceneEnum.Scene0;

        protected override Task OnInitializedAsync()
        {
            _content =
                "- привет" + Environment.NewLine +
                "- привет" + Environment.NewLine +
                "- как отчет?" + Environment.NewLine +
                "- хорошо, сам как?" + Environment.NewLine +
                "- да в целом неплохо. что собираешься делать?" + Environment.NewLine +
                "- посижу поработаю, а дальше хз" + Environment.NewLine +
                "- план хороший" + Environment.NewLine +
                "- да, мне тоже нравится" + Environment.NewLine +
                "- ну ладно, я погнал" + Environment.NewLine +
                "- а ты куда?" + Environment.NewLine +
                "- да в деревушку" + Environment.NewLine +
                "- хэх, забавно" + Environment.NewLine +
                "- ага, желаю удачи" + Environment.NewLine +
                "- удачи";

            Scene0 = new List<Npc>()
            {
                new () { Id = Guid.Parse("592804aa-c8e4-4c19-9f1d-3d991206155b"), Name = "Alex"},
                new () { Id = Guid.Parse("7e7424be-e274-456e-aa86-505c876b717d"), Name = "Dimon"},
                new () { Id = Guid.Parse("410b834e-2bee-4ac4-8605-e63ff516e5b7"), Name = "Maria"},
                new () { Id = Guid.Parse("00e73c35-d962-4a6b-af6b-79e39105b051"), Name = "Max"},
                new () { Id = Guid.Parse("f533c766-23af-4876-bfa0-6c8433792b4d"), Name = "Anna"},
                new () { Id = Guid.Parse("79dd79a3-f63c-4012-844e-c39a08239e0e"), Name = "Vladimir"},
                new () { Id = Guid.Parse("64d9abed-1624-47a1-a6aa-73aba12d3f01"), Name = "Nicolay"},
                new () { Id = Guid.Parse("a172ceb1-268f-46e5-b0f7-57a62f456e68"), Name = "Elena"},
                new () { Id = Guid.Parse("24a82e9f-9209-45cb-8aac-8232aa2fa995"), Name = "Tatyana"},
                new () { Id = Guid.Parse("d6ae1e55-ab97-41bb-8250-865dcbd1d80b"), Name = "Ekaterina"}
            };

            Scene1 = new List<Npc>()
            {
                new () { Id = Guid.Parse("4385c047-4e48-4ebd-b1a4-8a5d0c630786"), Name = "Dad"},
                new () { Id = Guid.Parse("6c9bd3ed-42bb-4c00-938b-eabe6369506a"), Name = "Mom"},
                new () { Id = Guid.Parse("0b80c75f-d125-4474-ac9a-e370eae24ff4"), Name = "Roman"},
                new () { Id = Guid.Parse("bfc32de0-25b7-413e-b9c8-7c3f5d8e89b2"), Name = "Kate"},
                new () { Id = Guid.Parse("f1c92ec5-cf3b-402d-ad34-bc1a83d1bbcc"), Name = "Liza"},
            };

            _model.Content = _content;
            _model.OnUpdateAllEvent += Refresh;

            return base.OnInitializedAsync();
        }

        private void Refresh()
        {
            StateHasChanged();
        }

        private void OnContentChanged(string value)
        {
            if (!_model.Content.Equals(value))
            {
                _model.Content = value;

                _dialogueCreationController.CreateMessageList(_model);
            }
        }


        public void Dispose()
        {
            _model.OnUpdateAllEvent -= Refresh;
        }
    }
}

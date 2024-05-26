using DialogueCreationKit.DialogueKit.Domain.Contracts;
using DialogueCreationKit.DialogueKit.Domain.Contracts.Services;
using DialogueCreationKit.DialogueKit.Domain.Enums;
using DialogueCreationKit.DialogueKit.Domain.Model.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DialogueCreationKit.DialogueKit.View.Components
{
    public partial class MessageView : IDisposable
    {
        [Inject]
        IDialogueCreationModel _model { get; set; }
        [Inject]
        DialogueCreationController _dialogueCreationController { get; set; }
        [Inject]
        DragAndDropController _dragAndDropController { get; set; }

        [Parameter]
        public EventCallback OnMessageClick { get; set; }

        [Parameter]
        public DialogueMessageView Message { get; set; }

        [Parameter]
        public bool IsCheckedMessageMode { get; set; } = true;

        [Parameter]
        public bool IsDraggable { get; set; } = true;


        private string colorAvatar0 = "background-color: #ff8080";
        private string colorAvatar1 = "background-color: #8080ff";

        private string colorBegin = "#b3efb7";
        private string[] colorContent = { "#7FFFD4", "#9ACEEB" };
        private string colorEnd = "#efbeb7";
        private string colorDefault = "#ffffff";

        private bool _isActor => Message.Id % 2 == 0;

        private DialogueStageView _stage { get; set; }

        private string GetColor()
        {
            return _stage.Stage switch
            {
                DialogueStage.Begin => colorBegin,
                DialogueStage.Content => colorContent[_stage.IdTheme],
                DialogueStage.End => colorEnd,
                _ => colorDefault
            };
        }



        private void UpdateTheme()
        {
            _dialogueCreationController.UpdateTheme(_model, true);
        }

        private void OnMessageChanged(string value)
        {
            if (!Message.Message.Equals(value))
            {
                Message.Message = value;

                if (IsCheckedMessageMode)
                {

                }
                else
                {
                    _model.ListMessages[Message.Id] = Message;
                    _model.Content = "- " + string.Join("\n- ", _model.ListMessages.Select(x => x.Message));
                    _dialogueCreationController.CreateMessageList(_model);
                }
                _model.OnUpdateAll();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _stage = _model.ListStage[Message.Id];
            _stage.OnUpdateThemeEvent += UpdateTheme;
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        public void Dispose()
        {
            _stage.OnUpdateThemeEvent -= UpdateTheme;
        }
    }
}

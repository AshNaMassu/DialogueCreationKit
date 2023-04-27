using Blazor.Diagrams.Core.Models.Base;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Blazor.Diagrams.Core.Geometry;
using DialogueCreationKit.Dialogue.Models.Diagram;
using DialogueCreationKit.Dialogue.Components;
using DialogueCreationKit.Dialogue.Models.Enums;

namespace DialogueCreationKit.Dialogue.Pages
{
    public partial class Main : IDisposable
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public Diagram Diagram { get; } = new Diagram(new DiagramOptions
        {
            GridSize = 40,
            AllowMultiSelection = false,
            Links = new DiagramLinkOptions
            {
                Factory = (diagram, sourcePort) =>
                {
                    return new LinkModel(sourcePort, null)
                    {
                        Router = Routers.Orthogonal,
                        PathGenerator = PathGenerators.Straight
                    };
                },
               // EnableSnapping = true
            }
        });

        public void Dispose()
        {
            Diagram.Links.Added -= OnLinkAdded;
            Diagram.Links.Removed -= Diagram_LinkRemoved;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Diagram.RegisterModelComponent<DialogueNodeModel, DialogueNode>();
            Diagram.Nodes.Add(new DialogueNodeModel( new DialogueMessageView(1) { Id = Guid.NewGuid(), MessageContent = "Text0 text0 text0", Stage = DialogueStage.Begin}, new Point(50, 000)));
            Diagram.Nodes.Add(new DialogueNodeModel( new DialogueMessageView(2) { Id = Guid.NewGuid(), MessageContent = "Text1 text1 text1", Stage = DialogueStage.Content}, new Point(50, 200)));
            Diagram.Nodes.Add(new DialogueNodeModel( new DialogueMessageView() { Id = Guid.NewGuid(), MessageContent = "Text2 text2 text2", Stage = DialogueStage.End}, new Point(50, 400)));

            Diagram.Links.Added += OnLinkAdded;
            Diagram.Links.Removed += Diagram_LinkRemoved;
        }

        private void OnLinkAdded(BaseLinkModel link)
        {
            link.TargetPortChanged += OnLinkTargetPortChanged;
        }

        private void OnLinkTargetPortChanged(BaseLinkModel link, PortModel oldPort, PortModel newPort)
        {
            //link.Labels.Add(new LinkLabelModel(link, "1..*", -40, new Point(0, -30)));
            link.Refresh();

            ((newPort ?? oldPort) as DialoguePortModel).DialogueMessage.Refresh();
        }

        private void Diagram_LinkRemoved(BaseLinkModel link)
        {
            link.TargetPortChanged -= OnLinkTargetPortChanged;

            if (!link.IsAttached)
                return;

            var sourceCol = (link.SourcePort as DialoguePortModel).DialogueMessage;
            var targetCol = (link.TargetPort as DialoguePortModel).DialogueMessage;
            ///TODO
            //(sourceCol.Primary ? targetCol : sourceCol).Refresh();
            (true ? targetCol : sourceCol).Refresh();
        }
    }
}

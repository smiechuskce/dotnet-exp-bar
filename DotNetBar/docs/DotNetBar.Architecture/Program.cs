using Structurizr;
using Structurizr.Api;

namespace DotNetBar.Architecture
{
    public class GettingStarted
    {
        public static void Main()
        {
            var workspace = new Workspace("Getting Started", "This is a model of my software system.");
            var model = workspace.Model;

            var bar = model.AddPerson("Bar", "Single bar using system");
            var system = model.AddSoftwareSystem("System", "Warehouse order managing software system.");
            bar.Uses(system, "Uses");

            var viewSet = workspace.Views;
            var contextView = viewSet.CreateSystemContextView(system, "SystemContext", "SystemContext diagram");
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            var styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle(Tags.SoftwareSystem)
            {
                Background = "#1168bd",
                Color = "#ffffff"
            });

            styles.Add(new ElementStyle(Tags.Person)
            {
                Background = "#08427b",
                Color = "#ffffff",
                Shape = Shape.Person
            });

            var structurizrClient = new StructurizrClient("e529355d-e5fb-4dce-ad0d-659ceb61a986",
                "6557a669-d8ab-4e14-9b51-d2131f93dec1");

            structurizrClient.PutWorkspace(87342, workspace);
        }
    }
}
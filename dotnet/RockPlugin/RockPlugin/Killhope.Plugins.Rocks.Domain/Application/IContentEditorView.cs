namespace Killhope.Plugins.Rocks.Presentation
{
    public interface IContentEditorView
    {
        string Data { get; set; }
        string Title { get; set; }

        void ShowDialog();
    }
}
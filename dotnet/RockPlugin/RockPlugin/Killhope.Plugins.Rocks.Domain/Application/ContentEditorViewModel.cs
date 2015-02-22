using System;
using Killhope.Plugins.Rocks.Domain;
using System.ComponentModel.Composition;

namespace Killhope.Plugins.Rocks.Presentation
{
    [Export]
    public class ContentEditorViewModel
    {
        private readonly IContentEditorView view;


        private Content content;

        public Content Content {
            get { return content; }
            set
            {

                this.content = value;
                if (value == null)
                    return;
                view.Title = value.Title;
                view.Data = value.Data;
            }
        }


        [ImportingConstructor]
        public ContentEditorViewModel(IContentEditorView view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));
            this.view = view;
        }

        public void ShowDialog()
        {
            this.view.ShowDialog();
            this.content.Data = view.Data;
            this.content.Title = view.Title; 
        }
    }
}
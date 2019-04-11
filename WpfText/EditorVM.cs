using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfText
{
    public class EditorVM : BindableBase
    {
        private readonly IDocument document;
        private readonly TextArea textArea;

        public EditorVM(IDocument document, TextArea textArea)
        {
            this.document = document;
            this.textArea = textArea;
            textArea.PreviewKeyDown += TextArea_PreviewKeyDown;
            textArea.PreviewMouseLeftButtonUp += TextArea_PreviewMouseLeftButtonUp; ;
        }

        private void TextArea_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            document.Insert(0, "dssd");
        }

        private void TextArea_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
        }
    }
}

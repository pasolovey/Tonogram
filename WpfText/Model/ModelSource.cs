using RenderTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfText.Model
{
    class ModelSource : IModelSource
    {
        private readonly ICSharpCode.AvalonEdit.TextEditor textEditor;
        public event EventHandler ModelChanged;

        public ModelSource(ICSharpCode.AvalonEdit.TextEditor textEditor)
        {
            this.textEditor = textEditor;
            this.textEditor.Document.TextChanged += Document_TextChanged;
        }

        private void Document_TextChanged(object sender, EventArgs e)
        {
            ModelChanged?.Invoke(this, EventArgs.Empty);
        }

        public IEnumerable<ModelItem> Get()
        {
            var text = textEditor.Document.Text;
            Parser parser = new Parser(text);
            var commands = parser.Parse();
            List<ModelItem> res = new List<ModelItem>();
            ModelItem prevItem = null;
            foreach (var cmd in commands)
            {
                if (string.IsNullOrWhiteSpace(cmd.command))
                    continue;
                ModelItem item = null;
                try
                {
                    item = Map(cmd, prevItem);
                }
                catch (Exception e)
                {

                }

                if (item != null)
                {
                    prevItem = item;
                    res.Add(item);
                }
               
            }
            return res;
        }

        ModelItem Map(Parser.Com command, ModelItem prev)
        {
            var cmd = command.command;
            int s = 0, e = 0;
            string res = "";
            (s, e, res) = TryGetLevels(cmd);

            ModelItem item = null;

            if (res == "*")
            {
                e = s = Math.Max(e, s);
                if (prev != null && s == 0)
                {
                    s = e = prev.End;
                }
                item = new ModelItem() { Text = command.text, Type = 2, Start = s, End = e };
            }
            if (res == "#")
            {
                e = s = Math.Max(e, s);
                if (prev != null && s == 0)
                {
                    s = e = prev.End;
                }
                item = new ModelItem() { Text = command.text, Type = 1, Start = s, End = e };
            }

            if (res == "lf")
            {
                item = new ModelItem() { Text = command.text, Type = 3, Start = 3, End = 1 };
            }
            if (res == "mf")
            {
                item = new ModelItem() { Text = command.text, Type = 3, Start = 5, End = 1 };
            }
            if (res == "hf")
            {
                item = new ModelItem() { Text = command.text, Type = 3, Start = 9, End = 1 };
            }
            if (res == "lr")
            {
                item = new ModelItem() { Text = command.text, Type = 3, Start = 1, End = 3 };
            }
            if (res == "mr")
            {
                item = new ModelItem() { Text = command.text, Type = 3, Start = 3, End = 5 };
            }
            if (res == "hr")
            {
                item = new ModelItem() { Text = command.text, Type = 3, Start = 5, End = 9 };
            }

            return item;
        }

        (int,int, string) TryGetLevels(string cmd)
        {
            int s = 0, e = 0;
            string res = cmd;
            if (char.IsDigit(res[0]))
            {
                s = Convert.ToInt32(res[0].ToString());
                res = res.Remove(0);
            }
            if (char.IsDigit(res[res.Length - 1]))
            {
                e = Convert.ToInt32(res[res.Length - 1].ToString());
                res = res.Remove(res.Length - 1);
            }
            return (s, e, res);
        }
    }

}

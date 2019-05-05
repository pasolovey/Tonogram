namespace WpfText.Model
{
    using System;
    using System.Collections.Generic;

    internal class Parser
    {
        private readonly string text;
        private readonly List<char> comSymbols = new List<char>() { '|', '%', '*', '#', '/' };
        private readonly string[] split = new string[] { "/t", " "};
        private int curPos = 0;

        public Parser(string text)
        {
            this.text = text;
        }

        public List<ParserCommand> Parse()
        {
            var words = Split();
            List<ParserCommand> cmds = new List<ParserCommand>();
            foreach (string word in words)
            {
                string command = "";
                string part = "";
                bool prevIsSpecial = false;
                for (int pos = 0; pos < word.Length; pos++)
                { 
                    var letter = word[pos];
                    var isSpecial = IsSpecial(letter);
                    if (isSpecial && prevIsSpecial)
                    {
                        command += letter;
                        if (command != "" && pos == word.Length - 1)
                            cmds.Add(new ParserCommand() { Command = UnMacro(command), Text = part });
                        continue;
                    }
                    prevIsSpecial = isSpecial;
                    if (!isSpecial)
                    {
                        prevIsSpecial = false;
                        part += letter;
                    }
                    if (isSpecial)
                    {
                        var t = (command == "|" || command == "||") && letter == '|';
                        if (command != "" && !t)
                        {
                            cmds.Add(new ParserCommand() { Command = UnMacro(command), Text = part });
                            command = "";
                            part = "";
                        }

                        command += letter;
                        bool inMacro = IsMacro(letter);
                        while (inMacro && pos < word.Length - 1)
                        {
                            pos++;
                            command += word[pos];
                            if (IsMacro(word[pos]))
                            {
                                inMacro = false;
                            }
                        }
                    }

                    if (command != "" && pos == word.Length - 1)
                        cmds.Add(new ParserCommand() { Command = UnMacro(command), Text = part });
                }
            }

            return cmds;
        }

        private string UnMacro(string value)
        {
            return value.Replace("%", string.Empty);
        }

        private bool IsDigit(char value)
        {
            return char.IsDigit(value);
        }

        private bool IsMacro(char value)
        {
            return value == '%';
        }

        private bool IsSpecial(char value)
        {
            return value == '*' || value == '#' || value == '%' || value == '|' || value == '/' || char.IsDigit(value); ;
        }

        private bool IsCom(char value)
        {
            return comSymbols.Contains(value);
        }

        private string[] Split()
        {
            return Clean().Split(split, StringSplitOptions.RemoveEmptyEntries);
        }

        private string Clean()
        {
            return text.Replace(Environment.NewLine, " ");
        }

        public class ParserCommand
        {
            public string Command;
            public string Text;
        }
    }
}

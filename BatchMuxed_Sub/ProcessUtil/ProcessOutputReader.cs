using System;

namespace BatchMuxer_Sub.ProcessUtil
{
    public class ProcessOutputReader   // Optional, to get the output while executing instead only as result at the end
    {
        public event TextEventHandler OutputChanged;
        public event TextEventHandler OutputErrorChanged;
        public void UpdateOutput(string text)
        {
            OutputChanged?.Invoke(this, new TextEventArgs(text));
        }
        public void UpdateOutputError(string text)
        {
            OutputErrorChanged?.Invoke(this, new TextEventArgs(text));
        }
        public delegate void TextEventHandler(object sender, TextEventArgs e);
        public class TextEventArgs : EventArgs
        {
            public string Text { get; }
            public TextEventArgs(string text) { Text = text; }
        }
    }
}

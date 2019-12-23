using System.IO;

namespace InstallPluginShowMessage
{
    public class ShowMessageButton
    {
        public string Text { get; set; }
        public string Command { get; set; }
        public string SmallBitmapPath { get; set; }
        public string LargeBitmapPath { get; set; }
        public string Tooltip { get; set; }

        public ShowMessageButton(string text, string command, string smallBitmapFileName, string largeBitmapFileName, string tooltip)
        {
            string bitmapPath = Utils.GetResourceBitmapPath();

            Text = text;
            Command = command;
            SmallBitmapPath = Path.Combine(bitmapPath, smallBitmapFileName);
            LargeBitmapPath = Path.Combine(bitmapPath, largeBitmapFileName);
            Tooltip = tooltip;
        }
    }
}

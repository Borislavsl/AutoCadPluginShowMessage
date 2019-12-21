using System.IO;
using System.Reflection;

namespace AutoCADpluginShowMessage
{
    internal static class Utils
    {
        public struct CommandButton
        {
            public string Text;
            public string Command;
            public string SmallBitmapPath;
            public string LargeBitmapPath;
            public string Tooltip;
         
            public CommandButton(string text, string command, string smallBitmapFileName, string largeBitmapFileName, string tooltip)
            {
                Text = text;
                Command = command;
                SmallBitmapPath = Path.Combine(GetResourceBitmapPath(), smallBitmapFileName);
                LargeBitmapPath = Path.Combine(GetResourceBitmapPath(), largeBitmapFileName);
                Tooltip = tooltip;
            }
        }

        internal static string GetResourceBitmapPath()
        {
            var uri = new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var projectFolder = new DirectoryInfo(uri).Parent.Parent.Parent.FullName;
            var bitmapFolder = Path.Combine(projectFolder, "Resources", "Bitmaps");

            return bitmapFolder;
        }
    }
}

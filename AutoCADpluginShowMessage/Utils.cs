using System.IO;
using System.Reflection;

namespace AutoCADpluginShowMessage
{
    internal static class Utils
    {
        internal static string GetResourceBitmapPath()
        {
            var uri = new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var projectFolder = new DirectoryInfo(uri).Parent.Parent.Parent.FullName;
            var bitmapFolder = Path.Combine(projectFolder, "Resources", "Bitmaps");

            return bitmapFolder;
        }
    }
}

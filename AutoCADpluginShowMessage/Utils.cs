using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

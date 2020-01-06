using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace ShowMessageUtilities
{
    public static class Services
    {
        public static void OpenNewDrawing(string strTemplatePath)
        {
            DocumentCollection acDocMgr = Application.DocumentManager;
            Document acDoc = acDocMgr.Add(strTemplatePath);

            acDocMgr.MdiActiveDocument = acDoc;
        }

        public static void OpenLayout(string layoutName)
        {
            // Reference the Layout Manager
            LayoutManager acLayoutMgr = LayoutManager.Current;

            acLayoutMgr.CurrentLayout = layoutName;
        }
    }
}

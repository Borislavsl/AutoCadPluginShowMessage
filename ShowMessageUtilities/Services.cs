using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace ShowMessageUtilities
{
    public class Services
    {
        public void OpenNewDrawing(string strTemplatePath)
        {
            DocumentCollection acDocMgr = Application.DocumentManager;
            Document acDoc = acDocMgr.Add(strTemplatePath);

            acDocMgr.MdiActiveDocument = acDoc;
        }

        public void OpenLayout(string layoutName)
        {
            // Reference the Layout Manager
            LayoutManager acLayoutMgr = LayoutManager.Current;

            acLayoutMgr.CurrentLayout = layoutName;
        }
    }
}

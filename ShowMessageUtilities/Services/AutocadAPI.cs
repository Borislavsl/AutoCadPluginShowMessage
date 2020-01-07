using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace ShowMessageUtilities
{
    public static partial class AutocadAPI
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

        public static void AppendMTextToModelSpace(MText mText)
        {
            Database database = HostApplicationServices.WorkingDatabase;
            // Append entity to model space
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                var blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
                var blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                blockTableRecord.AppendEntity(mText);

                // Add the text to the transaction, and commit the changes.
                transaction.AddNewlyCreatedDBObject(mText, true);
                transaction.Commit();
            }
        }
    }
}

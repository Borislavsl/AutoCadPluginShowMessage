using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;

namespace AutoCADpluginShowMessage
{
    public class ShowMessageCommands
    {
        // Defines a command which prompt a message on the AutoCAD command line
        [CommandMethod("SHOW")]
        public void ShowCommand()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nShow Message!\n");
        }

        // Defines a command which displays a Windows form
        [CommandMethod("SHOWFORM")]
        public void ShowFormCommand()
        {
            var dlg = new ShowMessageForm();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(dlg);
        }

        // Defines a command which adds a 'Show Text' MText object to Model Space
        [CommandMethod("ASDK", "SHOWTEXT", CommandFlags.Modal)]
        public void ShowTextCommand()
        {
            // ObjectARX generally reports errors through return values.
            // .NET uses exceptions 

            Database database = HostApplicationServices.WorkingDatabase;
            Transaction transaction = database.TransactionManager.StartTransaction();

            try
            {
                // Create new mtext and set text
                var text = new MText();
                text.Contents = "Show Text";

                // Append entity to model space
                var blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
                var blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                blockTableRecord.AppendEntity(text);

                // Add the text to the transaction, and commit the changes.
                transaction.AddNewlyCreatedDBObject(text, true);
                transaction.Commit();
            }
            catch
            {
                MessageBox.Show("Error in Show Text message");
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using MgdAcApplication = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace ShowMessageCommands
{
    public class Commands
    {
        // Defines a command which displays a Windows form
        [CommandMethod("SHOWBOXTEXT")]
        public void ShowFormCommand()
        {
            Editor editor = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                var dlg = new ShowMessageForm();
                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(dlg);
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage(Environment.NewLine + "Error in Show Box message: " + ex.Message);
            }
        }

        // Defines a command which prompt a message on the AutoCAD command line
        [CommandMethod("SHOWPROMPTTEXT")]
        public void ShowCommand()
        {
            MgdAcApplication.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nPlugin Message!\n");
        }

        // Defines a command which adds a 'Plugin Text' MText object to Model Space
        [CommandMethod("ASDK", "SHOWMODELTEXT", CommandFlags.Modal)]
        public void ShowTextCommand()
        {
            // ObjectARX generally reports errors through return values.
            // .NET uses exceptions 
            Editor editor = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor;

            Database database = HostApplicationServices.WorkingDatabase;
            Transaction transaction = database.TransactionManager.StartTransaction();

            try
            {
                // Create new mtext and set text
                var text = new MText();
                text.Contents = "Plugin Text";

                // Append entity to model space
                var blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
                var blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                blockTableRecord.AppendEntity(text);

                // Add the text to the transaction, and commit the changes.
                transaction.AddNewlyCreatedDBObject(text, true);
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage(Environment.NewLine + "Error in Show Text message: " + ex.Message);
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}

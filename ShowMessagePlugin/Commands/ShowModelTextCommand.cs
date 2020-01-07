using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using ShowMessageUtilities;

namespace ShowMessagePlugin.Commands
{
    public class ShowModelTextCommand
    {
        // Defines a command which adds an MText object to Model Space
        [CommandMethod("ASDK", "SHOW_MODEL_TEXT", CommandFlags.Modal)]
        public void ShowModelText()
        {         
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                // Create a new mtext and set the text
                var text = new MText();
                text.Contents = "Plugin Text";

                Database database = HostApplicationServices.WorkingDatabase;
                // Append entity to model space
                using (Transaction transaction = database.TransactionManager.StartTransaction())
                {
                    var blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    var blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    blockTableRecord.AppendEntity(text);

                    // Add the text to the transaction, and commit the changes.
                    transaction.AddNewlyCreatedDBObject(text, true);
                    transaction.Commit();
                }

                AutocadAPI.OpenLayout("Layout1");
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage(Environment.NewLine + "Error in Show Text message: " + ex.Message);
            }
        }
    }
}

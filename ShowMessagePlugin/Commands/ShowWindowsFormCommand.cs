using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using ShowMessageUtilities;

namespace ShowMessagePlugin.Commands
{
    public class ShowWindowsFormCommand
    {
        // Defines a command which displays a Windows form
        [CommandMethod("SHOW_WINDOWS_FORM")]
        public void ShowWindowsForm()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                var dlg = new WindowsForm();
                Application.ShowModalDialog(dlg);
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage(Environment.NewLine + "Error in Show Windows Form: " + ex.Message);
            }
        }
    }
}

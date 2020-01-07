using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;

namespace ShowMessagePlugin.Commands
{
    public class ShowPromptTextCommand
    {
        // Defines a command which prompts a message on the AutoCAD command line
        [CommandMethod("SHOW_PROMPT_TEXT")]
        public void ShowPromptText()
        {
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nPlugin Message!\n");
        }
    }
}

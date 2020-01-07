using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using static ShowMessageUtilities.Utils;
using ShowMessageUtilities;

[assembly: ExtensionApplication(typeof(UninstallPluginShowMessage.ShowMessageUninstall))]

namespace UninstallPluginShowMessage
{
    public class ShowMessageUninstall : IExtensionApplication
    {
        public void Initialize()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (AutocadAPI.RemoveRibbonPanel("ID_ADDINSTAB", PluginName))
                {
                    Application.ReloadAllMenus();                    

                    editor.WriteMessage(ProgramMessage[KEY_UNINS]);
                }
                else
                {
                    editor.WriteMessage(ProgramMessage[KEY_REMOV]);
                }               
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage(ProgramMessage[KEY_ERROR] + ex.Message);
            }
        }


        public void Terminate()
        {            
        }
    }
}

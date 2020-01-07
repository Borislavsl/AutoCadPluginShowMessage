using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Customization;
using static ShowMessageUtilities.Utils;
using ShowMessageUtilities;

[assembly: ExtensionApplication(typeof(ShowMessagePlugin.ShowMessageCreatePlugin))]
[assembly: CommandClass(typeof(ShowMessagePlugin.Commands.ShowWindowsFormCommand))]
[assembly: CommandClass(typeof(ShowMessagePlugin.Commands.ShowPromptTextCommand))]
[assembly: CommandClass(typeof(ShowMessagePlugin.Commands.ShowModelTextCommand))]

namespace ShowMessagePlugin
{
    public class ShowMessageCreatePlugin : IExtensionApplication
    {
        public void Initialize()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                RibbonPanelSource panelSource = AutocadAPI.AddRibbonPanel("ID_ADDINSTAB", PluginName, PluginText);
                if (panelSource == null)
                {
                    editor.WriteMessage(ProgramMessage[KEY_ADDED]);
                }
                else
                {
                    RibbonSplitButton splitButton = AutocadAPI.AddSplitButtonToRibbonPanel(panelSource);                   
                    
                    CustomizationSection custSection = panelSource.CustomizationSection;
                    MacroGroup macroGroup = custSection.MenuGroup.MacroGroups[0];

                    PluginCommandButton[] buttons = GetShowMessageButtons();
                    foreach (PluginCommandButton button in buttons)
                        AutocadAPI.AddCommandButtonToRibbonSplitButton(macroGroup, splitButton, button.Text, button.Command, button.SmallBitmapPath, button.LargeBitmapPath, button.Tooltip);

                    custSection.Save();

                    Application.ReloadAllMenus();                  

                    editor.WriteMessage(ProgramMessage[KEY_INST]);
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

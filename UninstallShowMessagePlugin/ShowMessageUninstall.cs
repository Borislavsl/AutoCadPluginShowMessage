using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Customization;
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
                if (RemoveRibbonPanel("ID_ADDINSTAB", PluginName))
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

        private bool RemoveRibbonPanel(string tabAlias, string panelAlias)
        {
            var fileName = Application.GetSystemVariable("MENUNAME") as string;
            var custSection = new CustomizationSection(fileName);
            RibbonRoot ribbonRoot = custSection.MenuGroup.RibbonRoot;

            RibbonPanelSource ribbonPanel = ribbonRoot.FindPanelWithAlias(panelAlias);
            if (ribbonPanel == null)
                return false;

            // Remove command and split buttons,  and panel row
            var row = ribbonPanel.Items[0] as RibbonRow;
            var splitButton = row.Items[0] as RibbonSplitButton;
            splitButton.Items.Clear();
            splitButton.SetIsModified();
            row.Items.Clear();
            row.SetIsModified();
            ribbonPanel.Items.Clear();
            ribbonPanel.SetIsModified();

            // Remove the reference to panel from Add-ins tab
            RibbonTabSource tab = ribbonRoot.FindTabWithAlias(tabAlias);
            RemoveRibbonPanelSourceReference(tab, ribbonPanel.ElementID);

            RemoveMacros(custSection);

            ribbonRoot.RibbonPanelSources.Remove(ribbonPanel);
            ribbonRoot.SetIsModified();

            custSection.Save();

            return true;
        }

        private void RemoveRibbonPanelSourceReference(RibbonTabSource tab, string elementID)
        {
            if (tab != null)
            {
                foreach (RibbonPanelSourceReference item in tab.Items)
                {
                    if (item.PanelId == elementID)
                    {
                        tab.Items.Remove(item);
                        return;
                    }
                }
            }
            tab.SetIsModified();
        }

        private void RemoveMacros(CustomizationSection custSection)
        {
            ShowMessageButton[] buttons = GetShowMessageButtons();

            string macroNames = string.Empty;
            foreach (var button in buttons)
                macroNames += button.Command + "_Macro ";

            int macrosToRemoveRemain = buttons.Length;

            MenuMacroCollection menuMacros = custSection.MenuGroup.MacroGroups[0].MenuMacros;
            for (int i = menuMacros.Count - 1; i >= 0; i--)
            {
                if (macroNames.Contains(menuMacros[i].macro.Name))
                {
                    menuMacros.Remove(menuMacros[i]);
                    macrosToRemoveRemain--;
                }
                if (macrosToRemoveRemain == 0)
                    break;
            }
            custSection.MenuGroup.MacroGroups[0].SetIsModified();
        }

        public void Terminate()
        {            
        }
    }
}

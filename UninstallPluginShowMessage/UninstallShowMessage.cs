using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Customization;
using static InstallPluginShowMessage.Utils;
using InstallPluginShowMessage;

[assembly: ExtensionApplication(typeof(UninstallPluginShowMessage.UninstallShowMessage))]

namespace UninstallPluginShowMessage
{
    public class UninstallShowMessage : IExtensionApplication
    {
        public void Initialize()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                RemoveRibbonPanel("ID_ADDINSTAB", PluginName);

                editor.WriteMessage(ProgramMessage[KEY_UNINS]);
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage(ProgramMessage[KEY_ERROR] + ex.Message);
            }
        }

        private void RemoveRibbonPanel(string tabAlias, string panelAlias)
        {
            var fileName = Application.GetSystemVariable("MENUNAME") as string;
            var custSection = new CustomizationSection(fileName);
            RibbonRoot ribbonRoot = custSection.MenuGroup.RibbonRoot;

            RibbonPanelSource ribbonPanel = ribbonRoot.FindPanelWithAlias(panelAlias);
            if (ribbonPanel == null)
                throw new System.Exception(ProgramMessage[KEY_REMOV]);

            // Remove command and split buttons and panel row
            var row = ribbonPanel.Items[0] as RibbonRow;
            var splitButton = row.Items[0] as RibbonSplitButton;
            splitButton.Items.Clear();
            row.Items.Clear();
            ribbonPanel.Items.Clear();

            // Remove the reference to panel from Add-ins tab
            RibbonTabSource tab = ribbonRoot.FindTabWithAlias(tabAlias);
            RemoveRibbonPanelSourceReference(tab, ribbonPanel.ElementID);

            RemoveMacros(custSection);

            ribbonRoot.RibbonPanelSources.Remove(ribbonPanel);

            custSection.Save();
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
        }

        public void Terminate()
        {
            
        }
    }
}

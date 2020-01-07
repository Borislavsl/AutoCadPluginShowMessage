using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Customization;

namespace ShowMessageUtilities
{
    public static partial class AutocadAPI
    {
        #region Add Ribbon panel, split and command buttons

        public static RibbonPanelSource AddRibbonPanel(string tabAlias, string panelAlias, string panelText)
        {
            var fileName = Application.GetSystemVariable("MENUNAME") as string;
            var custSect = new CustomizationSection(fileName);
            RibbonRoot ribbonRoot = custSect.MenuGroup.RibbonRoot;

            RibbonPanelSource ribbonPanel = ribbonRoot.FindPanelWithAlias(panelAlias);
            if (ribbonPanel != null)
                return null;

            ribbonPanel = new RibbonPanelSource(ribbonRoot);
            ribbonPanel.Aliases.Add(panelAlias);
            ribbonPanel.Name = panelAlias;
            ribbonPanel.Text = panelText;
            ribbonPanel.Items.Clear();
            ribbonRoot.RibbonPanelSources.Add(ribbonPanel);

            RibbonTabSource tab = ribbonRoot.FindTabWithAlias(tabAlias);
            var panelRef = new RibbonPanelSourceReference(tab);
            panelRef.PanelId = ribbonPanel.ElementID;
            tab.Items.Add(panelRef);

            return ribbonPanel;
        }

        public static RibbonSplitButton AddSplitButtonToRibbonPanel(RibbonPanelSource panelSource)
        {
            var ribbonRow = new RibbonRow();           
            panelSource.Items.Add(ribbonRow);

            var splitButton = new RibbonSplitButton(ribbonRow);
            splitButton.ButtonStyle = RibbonButtonStyle.LargeWithText;
            ribbonRow.Items.Add(splitButton);

            return splitButton;
        }

        public static void AddCommandButtonToRibbonSplitButton(MacroGroup macroGroup, RibbonSplitButton splitButton, string buttonText, string command, string smallBitmapPath, string largeBitmapPath, string toolTip)
        {
            MenuMacro menuMacro = macroGroup.CreateMenuMacro(command + "_Macro", "^C^C" + command, command + "_Tag", command + "_Help", MacroType.Any, smallBitmapPath, largeBitmapPath, command + "_Label_Id");

            var button = new RibbonCommandButton(splitButton);
            button.MacroID = menuMacro.ElementID;
            button.Text = buttonText;
            button.ButtonStyle = RibbonButtonStyle.SmallWithoutText;
            button.KeyTip = buttonText + " Key Tip";
            button.TooltipTitle = toolTip;

            splitButton.Items.Add(button);
        }

        #endregion


        #region Remove Ribbon panel with its buttons and commands

        public static bool RemoveRibbonPanel(string tabAlias, string panelAlias, PluginCommandButton[] buttons)
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

            RemoveMacrosOfCommandButtons(custSection, buttons);

            ribbonRoot.RibbonPanelSources.Remove(ribbonPanel);
            ribbonRoot.SetIsModified();

            custSection.Save();

            return true;
        }

        private static void RemoveRibbonPanelSourceReference(RibbonTabSource tab, string elementID)
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

        private static void RemoveMacrosOfCommandButtons(CustomizationSection custSection, PluginCommandButton[] buttons)
        {        
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

        #endregion
    }
}

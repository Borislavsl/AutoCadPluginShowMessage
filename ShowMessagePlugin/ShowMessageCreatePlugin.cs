using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Customization;
using static ShowMessageUtilities.Utils;
using ShowMessageUtilities;

[assembly: ExtensionApplication(typeof(ShowMessagePlugin.ShowMessageCreatePlugin))]
[assembly: CommandClass(typeof(ShowMessagePlugin.ShowMessageCommands))]

namespace ShowMessagePlugin
{
    public class ShowMessageCreatePlugin : IExtensionApplication
    {
        public void Initialize()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                RibbonPanelSource panelSource = AddRibbonPanel("ID_ADDINSTAB", PluginName);
                if (panelSource == null)
                {
                    editor.WriteMessage(ProgramMessage[KEY_ADDED]);
                }
                else
                {
                    panelSource.Text = PluginText;
                    panelSource.Name = panelSource.Text;

                    var firstRow = new RibbonRow();
                    panelSource.Items.Clear();
                    panelSource.Items.Add(firstRow);

                    var splitButton = new RibbonSplitButton(firstRow);
                    splitButton.ButtonStyle = RibbonButtonStyle.LargeWithText;
                    firstRow.Items.Add(splitButton);
                    
                    CustomizationSection custSection = panelSource.CustomizationSection;
                    MacroGroup macroGroup = custSection.MenuGroup.MacroGroups[0];

                    ShowMessageButton[] buttons = GetShowMessageButtons();
                    foreach (ShowMessageButton button in buttons)
                        AddCommandButton(macroGroup, splitButton, button.Text, button.Command, button.SmallBitmapPath, button.LargeBitmapPath, button.Tooltip);

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

        private RibbonPanelSource AddRibbonPanel(string tabAlias, string panelAlias)
        {
            var fileName = Application.GetSystemVariable("MENUNAME") as string;
            var custSect = new CustomizationSection(fileName);
            RibbonRoot ribbonRoot = custSect.MenuGroup.RibbonRoot;

            RibbonPanelSource ribbonPanel = ribbonRoot.FindPanelWithAlias(panelAlias);
            if (ribbonPanel != null)
                return null;

            ribbonPanel = new RibbonPanelSource(ribbonRoot);
            ribbonPanel.Aliases.Add(panelAlias);
            ribbonRoot.RibbonPanelSources.Add(ribbonPanel);

            RibbonTabSource tab = ribbonRoot.FindTabWithAlias(tabAlias);
            var panelRef = new RibbonPanelSourceReference(tab);
            panelRef.PanelId = ribbonPanel.ElementID;
            tab.Items.Add(panelRef);

            return ribbonPanel;
        }

        private void AddCommandButton(MacroGroup macroGroup, RibbonSplitButton splitButton, string buttonText, string command, string smallBitmapPath, string largeBitmapPath, string toolTip)
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

        public void Terminate()
        {
        }
    }
}

using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Customization;
using MgdAcApplication = Autodesk.AutoCAD.ApplicationServices.Core.Application;

[assembly: ExtensionApplication(typeof(AutoCADpluginShowMessage.ShowMessageApp))]

namespace AutoCADpluginShowMessage
{
    public class ShowMessageApp : IExtensionApplication
    {
        public void Initialize()
        {
            Editor editor = MgdAcApplication.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                string smallBitmapFileName = "autocad.png";

                var buttons = new ShowMessageButton[] { new ShowMessageButton("Message Box", "SHOWBOXTEXT",    smallBitmapFileName, "messagebox.webp", "Opens a Windows form"),
                                                    new ShowMessageButton("Prompt",          "SHOWPROMPTTEXT", smallBitmapFileName, "commandline.png", "Prompts text on command line"),
                                                    new ShowMessageButton("Model Space",     "SHOWMODELTEXT",  smallBitmapFileName, "modelspace.jpg",  "Add text to model space")
                                                      };

                RibbonPanelSource panelSrc = GetRibbonPanel("ID_ADDINSTAB", "ShowMessagePlugin");
                panelSrc.Text = "Show Message";
                panelSrc.Name = panelSrc.Text;

                var firstRow = new RibbonRow();
                panelSrc.Items.Clear();
                panelSrc.Items.Add(firstRow);

                var splitButton = new RibbonSplitButton(firstRow);
                splitButton.ButtonStyle = RibbonButtonStyle.LargeWithText;
                firstRow.Items.Add(splitButton);

                CustomizationSection custSection = panelSrc.CustomizationSection;
                MacroGroup macroGroup = custSection.MenuGroup.MacroGroups[0];

                foreach (ShowMessageButton button in buttons)
                    AddCommandButton(macroGroup, splitButton, button.Text, button.Command, button.SmallBitmapPath, button.LargeBitmapPath, button.Tooltip);

                custSection.Save();
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage(Environment.NewLine + ex.Message);
            }
        }

        private RibbonPanelSource GetRibbonPanel(string tabAlias, string panelAlias)
        {
            var fileName = MgdAcApplication.GetSystemVariable("MENUNAME") as string;
            var custSect = new CustomizationSection(fileName);
            RibbonRoot ribbonRoot = custSect.MenuGroup.RibbonRoot;

            RibbonPanelSource ribbonPanel = ribbonRoot.FindPanelWithAlias(panelAlias);
            if (ribbonPanel != null)
                throw new System.Exception("Show Message Ribbon Panel already is added." + Environment.NewLine +
                                           "Please reset the corresponding settings or NETLOAD ShowMessageCommans.dll");

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
            var button = new RibbonCommandButton(splitButton);
            button.Text = buttonText;


            MenuMacro menuMac = macroGroup.CreateMenuMacro(command + "_Macro", "^C^C" + command, command + "_Tag", command + "_Help", MacroType.Any, smallBitmapPath, largeBitmapPath, command + "_Label_Id");
            button.MacroID = menuMac.ElementID;

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

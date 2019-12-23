using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Customization;
using static InstallPluginShowMessage.Utils;

namespace InstallPluginShowMessage
{
    public class ShowMessageCommands
    {
        // Defines a command which displays a Windows form
        [CommandMethod("SHOWBOXTEXT")]
        public void ShowFormCommand()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;

            try
            {
                var dlg = new ShowMessageForm();
                Application.ShowModalDialog(dlg);
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage(Environment.NewLine + "Error in Show Box message: " + ex.Message);
            }
        }

        // Defines a command which prompt a message on the AutoCAD command line
        [CommandMethod("SHOWPROMPTTEXT")]
        public void ShowCommand()
        {
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nPlugin Message!\n");
        }

        // Defines a command which adds a 'Plugin Text' MText object to Model Space
        [CommandMethod("ASDK", "SHOWMODELTEXT", CommandFlags.Modal)]
        public void ShowTextCommand()
        {
            // ObjectARX generally reports errors through return values.
            // .NET uses exceptions 
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;

            Database database = HostApplicationServices.WorkingDatabase;
            Transaction transaction = database.TransactionManager.StartTransaction();

            try
            {
                // Create new mtext and set text
                var text = new MText();
                text.Contents = "Plugin Text";

                // Append entity to model space
                var blockTable = transaction.GetObject(database.BlockTableId, OpenMode.ForRead) as BlockTable;
                var blockTableRecord = transaction.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                blockTableRecord.AppendEntity(text);

                // Add the text to the transaction, and commit the changes.
                transaction.AddNewlyCreatedDBObject(text, true);
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                editor.WriteMessage(Environment.NewLine + "Error in Show Text message: " + ex.Message);
            }
            finally
            {
                transaction.Dispose();
            }
        }

        [CommandMethod("UNINSTALLSHOWMESSAGE")]
        public void Uninstall()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                if (RemoveRibbonPanel("ID_ADDINSTAB", PluginName))
                {
                    editor.WriteMessage(ProgramMessage[KEY_UNINS]);
                    Application.ReloadAllMenus();
                }
                else
                    editor.WriteMessage(ProgramMessage[KEY_REMOV]);
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
    }
}

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Interop;

[assembly: ExtensionApplication(typeof(AutoCADpluginShowMessage.ShowMessageApp))]
[assembly: CommandClass(typeof(AutoCADpluginShowMessage.ShowMessageCommands))]

namespace AutoCADpluginShowMessage
{
    public class ShowMessageApp : IExtensionApplication
    {
        public void Initialize()
        {
            // Create an AutoCAD tool bar with 3 buttons linked to 3 commands defined in ShowMessageCommands class

            string bitmapPath = Utils.GetResourceBitmapPath();
            var button0Path = bitmapPath + @"\tbBut0.bmp";
            var button1Path = bitmapPath + @"\tbBut1.bmp";

            var acadApp = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication as AcadApplication;
            AcadToolbar showMessageToolbar = acadApp.MenuGroups.Item(0).Toolbars.Add("Show Message");

            AcadToolbarItem tbBut0 = showMessageToolbar.AddToolbarButton(0, "Show", "Show Message - Show command", "_SHOW ");
            tbBut0.SetBitmaps(button0Path, button0Path);

            AcadToolbarItem tbBut1 = showMessageToolbar.AddToolbarButton(1, "ShowForm", "Show Message- ShowForm command", "_ShowForm ");
            tbBut1.SetBitmaps(button1Path, button1Path);

            AcadToolbarItem tbBut2 = showMessageToolbar.AddToolbarButton(2, "ShowText", "Show Message - Show command", "_SHOWTEXT ");
            tbBut2.SetBitmaps(button0Path, button0Path);
        }

        public void Terminate()
        {
        }
    }
}

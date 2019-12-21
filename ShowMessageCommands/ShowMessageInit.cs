using Autodesk.AutoCAD.Runtime;

[assembly: ExtensionApplication(typeof(ShowMessageCommands.ShowMessageInit))]
[assembly: CommandClass(typeof(ShowMessageCommands.Commands))]

namespace ShowMessageCommands
{
    public class ShowMessageInit : IExtensionApplication
    {
        public void Initialize()
        {            
        }

        public void Terminate()
        {            
        }
    }
}

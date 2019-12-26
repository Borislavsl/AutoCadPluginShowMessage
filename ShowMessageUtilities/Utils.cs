using System;
using System.Collections.Generic;
using System.IO;

namespace ShowMessageUtilities
{
    public class Utils
    {
        public const string KEY_INST = "Install";
        public const string KEY_ERROR = "Error";
        public const string KEY_ADDED = "Added";
        public const string KEY_UNINS = "Uninstall";
        public const string KEY_REMOV = "Removed";

        public static string PluginName = "ShowMessagePlugin";
        public static string PluginText = "Show Message";

        public static Dictionary<string, string> ProgramMessage = new Dictionary<string, string>()
                                                                    {   { KEY_INST,  Environment.NewLine +"Show Message Plugin is successfully installed." },

                                                                        { KEY_ADDED, Environment.NewLine +"Show Message Ribbon Panel is added." },

                                                                        { KEY_UNINS, Environment.NewLine +"Show Message Plugin is successfully uninstalled." },

                                                                        { KEY_REMOV, Environment.NewLine +"Show Message Ribbon Panel is already removed." + Environment.NewLine +
                                                                                 "Run NETLOAD command and select InstallPluginShowMessage.dll to install it." },

                                                                        { KEY_ERROR, Environment.NewLine +"Error occurred: " },
                                                                    };

        public static string GetPluginResourceDir()
        {
            string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            return Path.Combine(commonAppData, "Autodesk", "ApplicationPlugins", "ShowMessagePlugin.bundle", "Contents", "Resources");
        }

        public static ShowMessageButton[] GetShowMessageButtons()
        {
            string smallBitmapFileName = "autocad.png";

            var buttons = new ShowMessageButton[] { new ShowMessageButton("Message Box", "SHOWBOXTEXT",    smallBitmapFileName, "messagebox.webp", "Opens a Windows form"),
                                                    new ShowMessageButton("Prompt",          "SHOWPROMPTTEXT", smallBitmapFileName, "commandline.png", "Prompts text on command line"),
                                                    new ShowMessageButton("Model Space",     "SHOWMODELTEXT",  smallBitmapFileName, "modelspace.jpg",  "Add text to model space")
                                                      };
            return buttons;
        }
    }
}

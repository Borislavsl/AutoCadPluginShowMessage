using System;
using System.Collections.Generic;
using System.IO;

namespace ShowMessageUtilities
{
    public static class Utils
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

        public static PluginCommandButton[] GetShowMessageButtons()
        {
            string smallBitmapFileName = "autocad.png";

            var buttons = new PluginCommandButton[] { new PluginCommandButton("Windows Form", "SHOW_WINDOWS_FORM", smallBitmapFileName, "messagebox.webp", "Shows a Windows Form"),
                                                      new PluginCommandButton("Prompt",       "SHOW_PROMPT_TEXT",  smallBitmapFileName, "commandline.png", "Prompts text on command line"),
                                                      new PluginCommandButton("Model Space",  "SHOW_MODEL_TEXT",   smallBitmapFileName, "modelspace.jpg",  "Adds text to model space")
                                                    };
            return buttons;
        }       
    }
}

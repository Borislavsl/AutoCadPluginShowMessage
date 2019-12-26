# AutoCadShowMessagePlugin:


Start the project ShowMessagePlugin.

It creates the plugin in %ProgramData%\Autodesk\ApplicationPlugins\ShowMessagePlugin.bundle folder,
and starts Autocad. ShowMesssagePlugin.dll is automatically executed in %ProgramData%\Autodesk\ApplicationPlugins\ShowMessagePlugin.bundle\Contents folder.

In Add-ins a new panel and three split buttons are created. Clicking on them, they execute respectively:

- A command which displays a Windows form.

- A command which prompts a message on the AutoCAD command line.

- A command which adds a 'Plugin Text' MText object to Model Space.

Try each of them.

Close AutoCad.

To uninstall the plugin, start UninstallShowMessagePlugin project..

In Autocad,  open a new drawing.  In the command line enter NETLOAD and
select the UninstallShowMessagePlugin.dll  from the folder ......\AutoCadShowMessagePlugin\UninstallShowMessagePlugin\bin\debug.
The %ProgramData%\Autodesk\ApplicationPlugins\ShowMessagePlugin.bundle folder, the panel with three buttons and the corresponding macros are removed.  In this way the whole plugin is uninstalled.




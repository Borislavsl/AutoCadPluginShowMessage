# AutoCadShowMessagePlugin:

Prerequisites

1. Installed Autocad 2020

2. Installed ObjectARX_for_AutoCAD_2020_Win_64_bit from https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx-license-download

3. After open the solution in Visual Studio,
the references to the following Dlls:
AcCoreMgd
AcCui
AcDbMgd
AcMgd,

should be added from the folder: 
C:\Autodesk\ObjectARX_for_AutoCAD_2020_Win_64_bit\inc\

to both projects
ShowMessagePlugin and
UninstallShowMessagePlugin.

4. In Properties Debug section of both projects  "Start external program" should be checked with 
C:\Program Files\Autodesk\AutoCAD 2020\acad.exe

Start the project ShowMessagePlugin.

It creates the plugin in %ProgramData%\Autodesk\ApplicationPlugins\ShowMessagePlugin.bundle\ folder and subfolders (by post build events), and starts Autocad. ShowMesssagePlugin.dll is automatically executed from %ProgramData%\Autodesk\ApplicationPlugins\ShowMessagePlugin.bundle\Contents\ folder.

In Add-ins a new panel containing a split button with three command buttons are added. Clicking on them, they execute respectively:

- A command which displays a Windows form.

- A command which prompts a message on the AutoCAD command line.

- A command which adds a 'Plugin Text' MText object to Model Space (Open the Layout1).

Try each of them.

Close AutoCad.

To uninstall the plugin, start UninstallShowMessagePlugin project..

In Autocad,  open a New Drawing.  In the command line enter NETLOAD and
select the UninstallShowMessagePlugin.dll  from the folder ......\AutoCadShowMessagePlugin\UninstallShowMessagePlugin\bin\debug.
The %ProgramData%\Autodesk\ApplicationPlugins\ShowMessagePlugin.bundle folder (it's deleted by pre-build events), the panel with three buttons and the corresponding macros are removed.  In this way, the whole plugin is uninstalled.




if not exist "%ProgramData%\Autodesk\ApplicationPlugins\$1.bundle" mkdir  "%ProgramData%\Autodesk\ApplicationPlugins\%1.bundle"
if not exist "%ProgramData%\Autodesk\ApplicationPlugins\%1.bundle\Contents" mkdir "%ProgramData%\Autodesk\ApplicationPlugins\%1.bundle\Contents"
if not exist "%ProgramData%\Autodesk\ApplicationPlugins\%1.bundle\Contents\Resources" mkdir  "%ProgramData%\Autodesk\ApplicationPlugins\%1.bundle\Contents\Resources"
xcopy   "%2*.dll" "%ProgramData%\Autodesk\ApplicationPlugins\%1.bundle\Contents" /Y
xcopy   "%3Resources\*.*"  "%ProgramData%\Autodesk\ApplicationPlugins\%1.bundle\Contents\Resources" /Y
xcopy   "%3*.xml"  "%ProgramData%\Autodesk\ApplicationPlugins\%1.bundle" /Y

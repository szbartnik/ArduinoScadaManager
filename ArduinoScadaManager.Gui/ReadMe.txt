1. Each slave module project library should be created in physical directory "ArduinoScadaManager\SlaveDevicesModules".
2. Each slave module project should be in "SlaveDevicesModule" solution directory.
3. Each slave module project should have following line in its post-build events:

xcopy "$(TargetPath)" "$(SolutionDir)ArduinoScadaManager.Gui\$(OutDir)Modules\" /Y
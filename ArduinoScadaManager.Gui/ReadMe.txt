######################################################################################
# Each slave module project library:
######################################################################################
1. Should be created in physical directory "ArduinoScadaManager\SlaveDevicesModules".
2. Should be in "SlaveDevicesModule" solution directory.
3. Should contain following line in its post-build events:

xcopy "$(TargetPath)" "$(SolutionDir)ArduinoScadaManager.Gui\$(OutDir)Modules\" /Y
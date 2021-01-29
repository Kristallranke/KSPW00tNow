# KSPW00tNow
This repo contains a plugin for Kerbal Space Program which adds support for Wooting keyboards (e.g. Wooting One/Two).
This plugin reads the analog values from the keyboard and applies those instead of the binary controls.

## Features
- Automatically binds to the primary keybindings
- Flight controls (rotate, translate, thrust)
- SAS override
- EVA: variable ladder climb speed
- EVA: variable walk and run speed (CameraFrameMode* on)
- EVA: 360° movement (CameraFrameMode* off)
- EVA: jetpack and parachute control

*) CameraFrameMode means whether the Kerbal faces the camera direction or the movement direction. Per default this can be toggled with the Alt key.

## Build
1. Update the assembly references according to the [Wiki page](https://wiki.kerbalspaceprogram.com/wiki/Setting_up_Visual_Studio) of Kerbal Space Program 
2. Choose configuration _Release_
3. Build the solution

## Installation
Copy the content of package\GameData to the GameData directory of the Kerbal Space Program install folder.

The file structure should look as following:\
\<KSPRootDir\>\GameData\KSPW00tNow\Plugins\0Harmony.dll\
\<KSPRootDir\>\GameData\KSPW00tNow\Plugins\KSPW00tNow.dll\
\<KSPRootDir\>\GameData\KSPW00tNow\Plugins\wooting_analog_wrapper.dll

## Usage
No manual setup is required.
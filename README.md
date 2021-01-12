# KSPW00tNow
This repo contains a plugin for Kerbal Space Program which adds support for Wooting Keyboards (e.g. Wooting One/Two).
This plugin reads the analog values from the keyboard and applies it on the flight controls.

## Build
1. Update the assembly references according to the [Wiki page](https://wiki.kerbalspaceprogram.com/wiki/Setting_up_Visual_Studio) of Kerbal Space Program 
2. Choose configuration _Release_
3. Build the solution

## Installation
Copy the content of package\GameData to the GameData directory of the Kerbal Space Program install folder.

The file structure should look as following:\
GameData\KSPW00tNow\Plugins\KSPW00tNow.dll
GameData\KSPW00tNow\Plugins\wooting_analog_wrapper.dll

## Usage
The plugin automatically binds to the primary keybindings. No manual setup is required.
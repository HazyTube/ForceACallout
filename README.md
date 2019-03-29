# Force A Callout

Force A Callout is an LSPDFR plugin that lets players force a callout by pressing a key.
Players can also change their availability by pressing a key.
The plugin also includes an on screen text box that shows the availability of the player.
The keybindings are all changeable in the .ini file that comes with the plugin.
Players also have the option to enable or disable the on screen text box in the .ini file.

## Getting Started

These instructions will help you get all the information you need to use this plugin.

### Requirements

What things you need to use the plugin.

```
RagePluginHook
LSPDFR
RageNativeUI (Included)
```

### Installing

Copy all of the contents included in the zip file to your main GTA 5 directory (These files are; RageNativeUI, and the Plugins folder)

### Using the ini file


#### Changing keybindings

You can find a list of valid keys here: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=netframework-4.6.1

To change keybindings, you simply change the value behind the = mark.
For example: ForceCalloutKey = X 
You could change this to ForceCalloutKey = Z

The modifier keys are secondary keys you need to press along with the primary key.
If for example the ForceCalloutModifier = LShiftKey then you need to press the left shift key along with the X key to force a callout

You can always set a key to None to disable it.

#### Changing settings

**DebugLogging** - set this to true if you want more detailed logging (only turn this on if you know what you are doing)

**AvailableForCalloutsText** - Enable or disable the on screen text that shows you if you are available for callouts or not (If you disable this you will get a notification instead)

**RectangleAlpha** - The alpha of the black rectangle under the text, 0 is fully transparent 255 is solid black. Default is 200

**CalloutProbability** - This will take the probability set by the callout developer into account when triggering a random callout (If you dont know what this is, leave it at true)

**CalloutProbabilityMultiplier** - This multipler is applied to the base probability when the above is enabled. Increase for better randomization (Leave it at 1 if you dont know what this is)

**StopCurrentCallout** - This allows you to replicate LSPDFR 0.3.1 behavior of force ending the current callout if one is running 

## Built With

* RagePluginHook - https://ragepluginhook.net/
* LSPDFR - https://www.lcpdfr.com/lspdfr/index/
* RageNativeUI - https://github.com/alexguirre/RAGENativeUI/releases

## License

This project is licensed under the GPL 3.0 License - see the [LICENSE.md](LICENSE.md) file for details

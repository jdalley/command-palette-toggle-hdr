# Toggle HDR Extension

This is an extension for Microsoft PowerToys' Command Palette tool (CmdPal). It enables you to toggle Windows HDR for one display at a time, unlike the global Win + Alt + B shortcut which toggles it for _all_ monitors. Handy if you like a shortcut to selectively enable HDR for some games but not others, and don't want to have it enabled on your other (non-primary) monitors.

https://learn.microsoft.com/en-us/windows/powertoys/command-palette

## Installation

### Command Palette

You can install the extension directly from Command Palette by running the `Install Command Palette extensions` item, then searching by extension name.

### Winget

[![WinGet Package Version](https://img.shields.io/winget/v/jdalley.ToggleHDRforCommandPalette)](https://winstall.app/apps/jdalley.ToggleHDRforCommandPalette)

You can install the extension using [Winget](https://learn.microsoft.com/en-us/windows/package-manager/winget/) with the following command:
```
winget install --id=jdalley.ToggleHDRforCommandPalette  -e
```

### Windows Store

It can also be installed directly from the Windows Store:

[![Microsoft](https://get.microsoft.com/images/en-us%20dark.svg)](https://apps.microsoft.com/detail/9pl3dp61zjtn)


## How To Use

After installing the extension, opening CmdPal, and starting to type "toggle" you will see one of two things:

1. A list of displays that have HDR capability, where selecting one will toggle Windows HDR for that display.
2. A message indicating no displays have HDR capability: `Toggle HDR - No displays support HDR`.

![ToggleHDRExtensionScreenshot](https://github.com/user-attachments/assets/b65c0187-3d6b-4069-b803-0a6405faa3e8)



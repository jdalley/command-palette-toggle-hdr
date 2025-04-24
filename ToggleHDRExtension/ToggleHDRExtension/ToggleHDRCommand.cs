using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using ToggleHDRExtension.Models;

namespace ToggleHDRExtension; 

internal sealed partial class ToggleHDRCommand : InvokableCommand
{
    public override string Name { get; set; } = "Toggle HDR";
    public override IconInfo Icon => new("\uE8A7");

    public int DisplayIndex { get; set; } = -1;
    public DisplayInfo DisplayInfo { get; set; } = new DisplayInfo();


    public ToggleHDRCommand(int displayIndex, DisplayInfo displayInfo)
    {
        DisplayIndex = displayIndex;
        DisplayInfo = displayInfo;
        Name = $"Toggle HDR for {displayInfo.DisplayName}";
    }

    public override CommandResult Invoke()
    {
        // Given the DisplayIndex, toggle the HDR state for the display using the HDRController
        try
        {
            HDRController.SetHDRStateForDisplay(DisplayIndex, !DisplayInfo.IsHDREnabled);
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., display not found, HDR not supported)
            var showMessageCommand = new ShowMessageCommand($"Error: {ex.Message}");
            showMessageCommand.Invoke();
            return CommandResult.KeepOpen();
        }

        return CommandResult.Dismiss();
    }
}

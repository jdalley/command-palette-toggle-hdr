using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using ToggleHDRExtension.Interop;
using ToggleHDRExtension.Models;

namespace ToggleHDRExtension; 

internal sealed partial class ToggleHDRCommand : InvokableCommand
{
    internal int DisplayIndex { get; } = -1;
    internal DisplayInfo DisplayInfo { get; } = new DisplayInfo();
    public override string Name { get; set; } = "Toggle HDR";
    public override IconInfo Icon => new("\uf19e"); // ToggleLeft

    internal ToggleHDRCommand(int displayIndex, DisplayInfo displayInfo)
    {
        DisplayIndex = displayIndex;
        DisplayInfo = displayInfo;
        Name = $"Toggle HDR for {displayInfo.DisplayName}";
    }

    public override CommandResult Invoke()
    {
        try
        {
            HDRController.SetHDRStateForDisplay(DisplayIndex, !DisplayInfo.IsHDREnabled);
            DisplayInfo.IsHDREnabled = !DisplayInfo.IsHDREnabled;
        }
        catch (Exception ex)
        {
            return CommandResult.ShowToast($"Error{ex.Message}");
        }

        return CommandResult.Dismiss();
    }
}

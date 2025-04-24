namespace ToggleHDRExtension.Models;

internal class DisplayInfo
{
    internal string DisplayName { get; set; }
    internal bool SupportsHDR { get; set; }
    internal bool IsHDREnabled { get; set; }
    internal Win32.LUID AdapterId { get; set; }
    internal uint DisplayId { get; set; }

    internal DisplayInfo()
    {
        DisplayName = "Unknown Display";
        SupportsHDR = false;
        IsHDREnabled = false;
    }
}

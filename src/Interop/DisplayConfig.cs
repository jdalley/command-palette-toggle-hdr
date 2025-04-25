using System;
using System.Runtime.InteropServices;

namespace ToggleHDRExtension.Interop;

internal static class DisplayConfig
{
    [DllImport("user32.dll")]
    internal static extern int GetDisplayConfigBufferSizes(
      QDC flags,
      out uint numPathArrayElements,
      out uint numModeInfoArrayElements);

    [DllImport("user32.dll")]
    internal static extern int QueryDisplayConfig(
        QDC flags,
        ref uint numPathArrayElements,
        [Out] DISPLAYCONFIG_PATH_INFO[] pathArray,
        ref uint numModeInfoArrayElements,
        [Out] DISPLAYCONFIG_MODE_INFO[] modeInfoArray,
        IntPtr currentTopologyId);

    [DllImport("user32.dll")]
    internal static extern int DisplayConfigSetDeviceInfo(
        ref DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE requestPacket);

    [DllImport("user32.dll")]
    internal static extern int DisplayConfigGetDeviceInfo(
        ref DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO requestPacket);

    [DllImport("user32.dll")]
    internal static extern int DisplayConfigGetDeviceInfo(
        ref DISPLAYCONFIG_TARGET_DEVICE_NAME requestPacket);
}

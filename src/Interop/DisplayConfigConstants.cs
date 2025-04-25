using System;

namespace ToggleHDRExtension.Interop;

internal static class DisplayConfigConstants
{
    internal const int ERROR_SUCCESS = 0;

    // DisplayConfig device info types
    internal const int DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME = 1;
    internal const int DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME = 2;
    internal const int DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE = 3;
    internal const int DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME = 4;
    internal const int DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE = 5;
    internal const int DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE = 6;
    internal const int DISPLAYCONFIG_DEVICE_INFO_GET_SUPPORT_VIRTUAL_RESOLUTION = 7;
    internal const int DISPLAYCONFIG_DEVICE_INFO_SET_SUPPORT_VIRTUAL_RESOLUTION = 8;
    internal const int DISPLAYCONFIG_DEVICE_INFO_GET_ADVANCED_COLOR_INFO = 9;
    internal const int DISPLAYCONFIG_DEVICE_INFO_SET_ADVANCED_COLOR_STATE = 10;
}

[Flags]
internal enum DisplayConfigGetAdvancedColorInfoValue : uint
{
    AdvancedColorSupported = 0x1,
    AdvancedColorEnabled = 0x2,
    WideColorEnforced = 0x4,
    AdvancedColorForceDisabled = 0x8
}

internal enum QDC : uint
{
    ALL_PATHS = 0x00000001,
    ONLY_ACTIVE_PATHS = 0x00000002,
    DATABASE_CURRENT = 0x00000004,
    VIRTUAL_MODE_AWARE = 0x00000010,
    INCLUDE_HMD = 0x00000020,
    FORCE_UINT32 = 0xFFFFFFFF
}

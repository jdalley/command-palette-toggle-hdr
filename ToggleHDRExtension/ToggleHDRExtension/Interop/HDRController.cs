using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ToggleHDRExtension.Models;

namespace ToggleHDRExtension.Interop;

/// <summary>
/// Getting and setting HDR state for displays.
/// </summary>
internal static class HDRController
{
    /// <summary>
    /// Set the HDR state for a specific display by its index.
    /// </summary>
    /// <param name="displayIndex"></param>
    /// <param name="enable">HDR State - On(true)/Off(false)</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    internal static void SetHDRStateForDisplay(int displayIndex, bool enable)
    {
        var displays = GetDisplays();

        if (displayIndex < 0 || displayIndex >= displays.Count)
        {
            throw new ArgumentException($"Display index {displayIndex} is out of range. Available displays: {displays.Count}");
        }

        var display = displays[displayIndex];

        if (!display.SupportsHDR)
        {
            throw new InvalidOperationException($"Display at index {displayIndex} does not support HDR");
        }

        // Set HDR state for the selected display
        var setAdvancedColorState = new DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE
        {
            header = new DISPLAYCONFIG_DEVICE_INFO_HEADER
            {
                type = DisplayConfigConstants.DISPLAYCONFIG_DEVICE_INFO_SET_ADVANCED_COLOR_STATE,
                size = (uint)Marshal.SizeOf<DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE>(),
                adapterId = display.AdapterId,
                id = display.DisplayId
            },
            value = enable ? 1u : 0u
        };

        int result = DisplayConfig.DisplayConfigSetDeviceInfo(ref setAdvancedColorState);
        if (result != DisplayConfigConstants.ERROR_SUCCESS)
        {
            throw new InvalidOperationException($"Failed to set HDR state for display {displayIndex}. Error code: {result}");
        }
    }

    /// <summary>
    /// Get a list of Displays/Monitors connected to this computer, including information on whether they 
    /// support HDR, and whether it's currently enabled.
    /// </summary>
    /// <returns><see cref="List{DisplayInfo}"/></returns>
    /// <exception cref="Exception"></exception>
    internal static List<DisplayInfo> GetDisplays()
    {
        // Get necessary buffer sizes for the display configuration
        int result = DisplayConfig.GetDisplayConfigBufferSizes(QDC.ONLY_ACTIVE_PATHS, out uint pathCount, out uint modeCount);
        if (result != DisplayConfigConstants.ERROR_SUCCESS)
        {
            throw new InvalidOperationException($"Failed to get display config buffer sizes. Error code: {result}");
        }

        // Query the display configuration
        DISPLAYCONFIG_PATH_INFO[] pathInfoArray = new DISPLAYCONFIG_PATH_INFO[pathCount];
        DISPLAYCONFIG_MODE_INFO[] modeInfoArray = new DISPLAYCONFIG_MODE_INFO[modeCount];
        result = DisplayConfig.QueryDisplayConfig(
            QDC.ONLY_ACTIVE_PATHS,
            ref pathCount,
            pathInfoArray,
            ref modeCount,
            modeInfoArray,
            nint.Zero);

        if (result != DisplayConfigConstants.ERROR_SUCCESS)
        {
            throw new InvalidOperationException($"Failed to query display configuration. Error code: {result}");
        }

        // Create list of display information
        var displays = new List<DisplayInfo>();

        for (int i = 0; i < pathCount; i++)
        {
            var displayInfo = new DisplayInfo
            {
                AdapterId = pathInfoArray[i].targetInfo.adapterId,
                DisplayId = pathInfoArray[i].targetInfo.id
            };

            // Get the display name
            var displayNameInfo = new DISPLAYCONFIG_TARGET_DEVICE_NAME
            {
                header = new DISPLAYCONFIG_DEVICE_INFO_HEADER
                {
                    type = DisplayConfigConstants.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME,
                    size = (uint)Marshal.SizeOf<DISPLAYCONFIG_TARGET_DEVICE_NAME>(), // Updated to use generic overload
                    adapterId = pathInfoArray[i].targetInfo.adapterId,
                    id = pathInfoArray[i].targetInfo.id
                }
            };

            result = DisplayConfig.DisplayConfigGetDeviceInfo(ref displayNameInfo);
            if (result == DisplayConfigConstants.ERROR_SUCCESS)
            {
                displayInfo.DisplayName = displayNameInfo.monitorFriendlyDeviceName;
            }

            // Check HDR capabilities
            var getAdvancedColorInfo = new DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO
            {
                header = new DISPLAYCONFIG_DEVICE_INFO_HEADER
                {
                    type = DisplayConfigConstants.DISPLAYCONFIG_DEVICE_INFO_GET_ADVANCED_COLOR_INFO,
                    size = (uint)Marshal.SizeOf<DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO>(), // Updated to use generic overload
                    adapterId = pathInfoArray[i].targetInfo.adapterId,
                    id = pathInfoArray[i].targetInfo.id
                }
            };

            result = DisplayConfig.DisplayConfigGetDeviceInfo(ref getAdvancedColorInfo);
            if (result == DisplayConfigConstants.ERROR_SUCCESS)
            {
                displayInfo.SupportsHDR = (getAdvancedColorInfo.value
                    & (uint)DisplayConfigGetAdvancedColorInfoValue.AdvancedColorSupported) != 0;
                displayInfo.IsHDREnabled = (getAdvancedColorInfo.value
                    & (uint)DisplayConfigGetAdvancedColorInfoValue.AdvancedColorEnabled) != 0;
            }

            displays.Add(displayInfo);
        }

        return displays;
    }
}

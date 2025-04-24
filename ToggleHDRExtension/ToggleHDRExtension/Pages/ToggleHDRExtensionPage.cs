// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System.Collections.Generic;
using System.Linq;

namespace ToggleHDRExtension;

internal sealed partial class ToggleHDRExtensionPage : ListPage
{
    public ToggleHDRExtensionPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "Toggle HDR";
        Name = "Open";
    }

    public override IListItem[] GetItems()
    {
        var displays = HDRController.GetDisplays();

        // Check if any display supports HDR
        bool anyDisplaySupportsHDR = displays.Any(display => display.SupportsHDR);
        if (!anyDisplaySupportsHDR)
        {
            var showMessageCommand = new ShowMessageCommand("No displays support HDR");
            return
            [
                new ListItem(showMessageCommand),
            ];
        }

        // Create a ToggleHDRCommand for each display
        var displayCommands = new List<ListItem>();
        for (int i = 0; i < displays.Count; i++)
        {
            var display = displays[i];
            var command = new ToggleHDRCommand(i, display);
            displayCommands.Add(new ListItem(command));
        }

        return [.. displayCommands];
    }
}

using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Runtime.InteropServices;

namespace ToggleHDRExtension;

internal sealed partial class ShowMessageCommand : InvokableCommand
{
    public override string Name => "Show message";
    public override IconInfo Icon => new("\uE8A7");
    public string Message { get; set; } = string.Empty;

    public ShowMessageCommand(string message)
    {
        Message = message;
    }

    public override CommandResult Invoke()
    {
        // 0x00001000 is MB_SYSTEMMODAL, which will display the message box on top of other windows.
        _ = MessageBox(0, Message, "ToggleHDR-Message", 0x00001000);
        return CommandResult.KeepOpen();
    }


    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
}

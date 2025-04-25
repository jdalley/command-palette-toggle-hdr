using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ToggleHDRExtension.Commands
{

    /// <summary>
    /// A command that only shows a message and does not perform any action.
    /// </summary>
    internal sealed partial class MessageOnlyCommand : InvokableCommand
    {
        public override string Name { get; set; } = string.Empty;
        public override IconInfo Icon => new("\ue8bd"); // Message

        internal MessageOnlyCommand(string message)
        {
            Name = message;
        }

        public override CommandResult Invoke()
        {
            return CommandResult.KeepOpen();
        }
    }

}

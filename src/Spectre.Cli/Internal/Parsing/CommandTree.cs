using System.Collections.Generic;
using Spectre.Cli.Exceptions;

namespace Spectre.Cli.Internal
{
    internal sealed class CommandTree
    {
        public CommandInfo Command { get; }
        public List<MappedCommandParameter> Mapped { get; }
        public List<CommandParameter> Unmapped { get; }
        public CommandTree? Parent { get; }
        public CommandTree? Next { get; set; }
        public bool ShowHelp { get; set; }

        public CommandTree(CommandTree? parent, CommandInfo command)
        {
            Parent = parent;
            Command = command;
            Mapped = new List<MappedCommandParameter>();
            Unmapped = new List<CommandParameter>();
        }

        public ICommand CreateCommand(ITypeResolver resolver)
        {
            if (Command.Delegate != null)
            {
                return new DelegateCommand(Command.Delegate);
            }

            if (resolver.Resolve(Command.CommandType) is ICommand command)
            {
                return command;
            }

            throw ParseException.CouldNotCreateCommand(Command.CommandType);
        }
    }
}
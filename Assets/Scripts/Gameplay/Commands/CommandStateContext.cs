using TowerDefence.Core;

namespace TowerDefence.Gameplay.Commands
{
    public struct CommandStateContext<ICommand> : IStateContext
    {
        public ICommand command;

        public CommandStateContext(ICommand command)
        {
            this.command = command;
        }
    }
}

using System;

namespace TowerDefence.Gameplay.Commands
{
    public interface ICommandCenter : IDisposable
    {
        void Subscribe<TCommand>(Action<TCommand> callback) where TCommand : ICommand;
        void Unsubscribe<TCommand>(Action<TCommand> callback) where TCommand : ICommand;
        void Tick(float deltaTime);
    }
}

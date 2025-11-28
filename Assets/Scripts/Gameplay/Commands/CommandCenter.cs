using System;
using System.Collections.Generic;

namespace TowerDefence.Gameplay.Commands
{
    public abstract class CommandCenter : ICommandCenter
    {
        private readonly Dictionary<Type, Delegate> _handlers = new();

        public void Subscribe<TCommand>(Action<TCommand> callback) where TCommand : ICommand
        {
            var type = typeof(TCommand);
            if (_handlers.TryGetValue(type, out var existing))
                _handlers[type] = Delegate.Combine(existing, callback);
            else
                _handlers[type] = callback;
        }

        public void Unsubscribe<TCommand>(Action<TCommand> callback) where TCommand : ICommand
        {
            var type = typeof(TCommand);
            if (_handlers.TryGetValue(type, out var existing))
            {
                var newDelegate = Delegate.Remove(existing, callback);
                if (newDelegate == null)
                    _handlers.Remove(type);
                else
                    _handlers[type] = newDelegate;
            }
        }

        protected void IssueCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var type = typeof(TCommand);
            if (_handlers.TryGetValue(type, out var existing)
                && existing is Action<TCommand> action)
            {
                action?.Invoke(command);
            }
        }

        public abstract void Tick(float deltaTime);
        public abstract void Dispose();
    }
}

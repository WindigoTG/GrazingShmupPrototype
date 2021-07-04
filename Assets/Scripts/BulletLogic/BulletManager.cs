using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class BulletManager : IUpdateableLate
    {
        private List<IBulletMoveCommand> _commands;

        public BulletManager()
        {
            _commands = new List<IBulletMoveCommand>();
        }

        public void LateUpdate(float deltaTime)
        {
            if (_commands.Count > 0)
                for (int i = _commands.Count-1; i >= 0; i--)
                    _commands[i].Execute(deltaTime);
        }

        public void AddCommand(IBulletMoveCommand command)
        {
            _commands.Add(command);
        }

        public void RemoveCommand(IBulletMoveCommand command)
        {
            if (_commands.Contains(command))
                _commands.Remove(command);
        }
    }
}
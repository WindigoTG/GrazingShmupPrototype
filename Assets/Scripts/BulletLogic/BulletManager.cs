using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    public class BulletManager : IUpdateableLate
    {
        private List<IBulletMoveCommand> _commands;
        float _maxTime = 0.01f;

        int _currentIndex;
        int _startIndex;
        bool _isNewCycle;
        float _startTime;

        public BulletManager()
        {
            _commands = new List<IBulletMoveCommand>();
        }

        public void LateUpdate(float deltaTime)
        {

            int bullets = 0;
            if (_commands.Count != 0)
            {
                _startIndex = _currentIndex;
                _isNewCycle = false;
                _startTime = Time.realtimeSinceStartup;


                do
                {
                    bullets++;
                    if (_commands[_currentIndex].Execute(deltaTime))
                    {
                        _currentIndex++;
                        if (_currentIndex >= _commands.Count)
                        {
                            _currentIndex = 0;
                            _isNewCycle = true;
                        }
                    }
                    else
                    {
                        _commands.Remove(_commands[_currentIndex]);
                        if (_currentIndex >= _commands.Count)
                        {
                            _currentIndex = 0;
                            _isNewCycle = true;
                        }
                        if (_commands.Count == 0)
                        {
                            _currentIndex = 0;
                            break;
                        }
                    }
                }
                while ((Time.realtimeSinceStartup - _startTime < _maxTime) && !(_currentIndex == _startIndex && _isNewCycle));
            }
            else
            {
                _currentIndex = 0;
            }
            Debug.Log($"Bullet Count: {_commands.Count}  |  FPS: {1 / Time.deltaTime}  |  {bullets}");
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
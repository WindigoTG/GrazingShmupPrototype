using System;
using UnityEngine;

namespace GrazingShmup
{
    public class EnemyEngine : IMovementEnemy
    {
        #region Fields

        Transform _enemy;
        Vector3[] _positions;
        int _currentPosition;

        EnemyMovementData _movementData;

        Vector3 _direction;

        public event Action ReachedFinish;

        #endregion


        #region ClassLifeCycles

        public EnemyEngine(EnemyMovementData movementData)
        {
            _movementData = movementData;
        }

        #endregion


        #region IMovementEnemy

        public void SetObjectToMoveAndRoute(Transform enemy, Vector3[] positions)
        {
            _enemy = enemy;
            _positions = positions;
            _currentPosition = 0;
        }

        public void Move(float deltaTime)
        {
            if ((_positions[_currentPosition] - _enemy.position).magnitude <= _movementData.MoveTreshold)
            {
                _currentPosition++;

                if (_currentPosition >= _positions.Length)
                {
                    ReachedFinish?.Invoke();
                    Restart();
                }
            }
            else
            {
                _direction = (_enemy.position - _positions[_currentPosition]).normalized;
                _enemy.Translate(_direction * _movementData.Speed * deltaTime);
            }
        }

        #endregion


        #region Methods

        private void Restart()
        {
            _enemy.position = _positions[0];
            _currentPosition = 0;
        }

        #endregion
    }
}
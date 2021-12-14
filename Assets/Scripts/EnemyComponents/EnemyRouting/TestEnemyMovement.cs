using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestEnemyMovement : MonoBehaviour
    {
        #region Fields

        [SerializeField] TestLineToBezierConversion _lineSource;
        [SerializeField] TestTransformToBezierConversion _routeSource;
        [SerializeField] float _speed;
        [SerializeField] float _treshold = 0.05f;

        Vector3[] _positions;
        int _currentPosition;

        Vector3 _direction;

        #endregion


        #region UnityMethods

        void Start()
        {
            _positions = _routeSource.GetRoute();
        }

        void Update()
        {
            _positions = _routeSource.GetRoute();

            if ((_positions[_currentPosition] - transform.position).magnitude <= _treshold)
            {
                _currentPosition++;

                if (_currentPosition >= _positions.Length)
                {
                    _currentPosition = 0;
                }
            }

            _direction = (transform.position - _positions[_currentPosition]).normalized;
            transform.Translate(_direction * _speed * Time.deltaTime);
        }

        #endregion
    }
}
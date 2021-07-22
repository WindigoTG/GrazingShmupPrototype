using UnityEngine;

namespace GrazingShmup
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet")]
    public sealed class BulletData : ScriptableObject
    {
        [Header("Single bullet settings")]
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletAngularSpeed;
        [SerializeField] private float _lifeTime;
        [Space]
        [SerializeField] private float _fireDelay;
        [Space]
        [Header("Line settings")]
        [SerializeField] private int _lineBulletCount;
        [SerializeField] private float _deltaSpeed;
        [Space]
        [Header("Arc settings")]
        [SerializeField] private int _arcLineCount;
        [SerializeField] private float _arcAngle;
        [SerializeField] private float _initialRadius;
        [Space]
        [Header("Row settings")]
        [SerializeField] private int _rowLineCount;
        [SerializeField] private float _rowLineOffset;
        [SerializeField] private float _rowVerticalOffset;
        [SerializeField] private bool _isMirrored;
        [Space]
        [Header("Row settings")]
        [SerializeField] private float _capsuleDalay;
        [SerializeField] private float _capsuleSpeed;
        [SerializeField] private float _capsuleAngularSpeed;

        public BulletConfig GetConfig()
        {
            return new BulletConfig(_bulletSpeed, _bulletAngularSpeed, _lifeTime, _fireDelay,
                _lineBulletCount, _deltaSpeed, _arcLineCount, _arcAngle, _initialRadius,
                _rowLineCount, _rowLineOffset, _rowVerticalOffset, _isMirrored,
                _capsuleDalay, _capsuleSpeed, _capsuleAngularSpeed);
        }
    }
}
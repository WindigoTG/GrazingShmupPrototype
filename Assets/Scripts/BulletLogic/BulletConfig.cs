using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct BulletConfig
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
        [Header("Delayed Capsule settings")]
        [SerializeField] private float _capsuleDelay;
        [SerializeField] private float _capsuleSpeed;
        [SerializeField] private float _capsuleAngularSpeed;

        private Vector3 _position;
        private Vector3 _rotation;
        private float _angle;

        public BulletConfig(float bulletSpeed = 1.0f, float bulletAngularSpeed = 0.0f, float lifetime = 5.0f,
                            float fireDelay = 1.0f, int lineBulletCount = 1, float deltaSpeed = 1.0f,
                            int arcLineCount = 1, float arcAngle = 0.0f, float initialRadius = 0.0f,
                            int rowLineCount = 1, float rowLineOffset = 0.0f, float rowVerticalOffset = 0.0f, bool isMirrored = true,
                            float capsuleDelay = 0.0f, float capsuleSpeed = 1.0f, float capsuleAngularSpeed = 0.0f)
        {
            _bulletSpeed = bulletSpeed;
            _bulletAngularSpeed = bulletAngularSpeed;
            _lifeTime = lifetime != 0 ? Math.Abs(lifetime) : 5.0f;
            _fireDelay = Math.Abs(fireDelay);
            _lineBulletCount = lineBulletCount > 0 ? lineBulletCount : 1;
            _deltaSpeed = deltaSpeed;
            _arcLineCount = arcLineCount > 0 ? arcLineCount : 1;
            _arcAngle = arcAngle;
            _initialRadius = initialRadius;
            _rowLineCount = rowLineCount > 0 ? rowLineCount : 1;
            _rowLineOffset = rowLineOffset;
            _rowVerticalOffset = rowVerticalOffset;
            _isMirrored = isMirrored;
            _capsuleDelay = capsuleDelay;
            _capsuleSpeed = capsuleSpeed;
            _capsuleAngularSpeed = capsuleAngularSpeed;
            _position = Vector3.zero;
            _rotation = Vector3.zero;
            _angle = 0.0f;
        }

        public float BulletSpeed
        {
            get => _bulletSpeed; 
            set => _bulletSpeed = value; 
        }

        public float BulletAngularSpeed
        {
            get => _bulletAngularSpeed * (float)Math.PI / 180; 
            set => _bulletAngularSpeed = value; 
        }

        public float LifeTime
        {
            get => _lifeTime; 
            set => _lifeTime = value; 
        }

        public float FireDelay
        {
            get => _fireDelay; 
            set => _fireDelay = Math.Abs(value); 
        }

        public int LineBulletCount
        {
            get => _lineBulletCount; 
            set => _lineBulletCount = value > 0 ? value : 1; 
        }

        public float DeltaSpeed
        {
            get => _deltaSpeed;
            set => _deltaSpeed = value;
        }

        public int ArcLineCount
        { 
            get => _arcLineCount;
            set => _arcLineCount = value > 0 ? value : 1;
        }

        public float ArcAngle
        {
            get => _arcAngle * (float)Math.PI / 180;
            set => _arcAngle = value;
        }

        public float InitialRadius
        {
            get => _initialRadius;
            set => _initialRadius = value;
        }

        public Vector3 Position
        {
            get => _position; 
            set => _position = value; 
        }

        public Vector3 Rotation
        {
            get => _rotation; 
            set => _rotation = value; 
        }

        public float Angle
        {
            get => _angle; 
            set => _angle = value; 
        }

        public int RowLineCount
        {
            get => _rowLineCount;
            set => _rowLineCount = value > 0 ? value : 1;
        }

        public float RowLineOffset
        {
            get => _rowLineOffset;
            set => _rowLineOffset = value;
        }

        public float RowVerticalOffset
        {
            get => _rowVerticalOffset;
            set => _rowVerticalOffset = value;
        }

        public bool IsMirrored
        {
            get => _isMirrored;
            set => _isMirrored = value;
        }

        public float CapsuleSpeed
        {
            get => _capsuleSpeed;
            set => _capsuleSpeed = value;
        }

        public float CapsuleAngularSpeed
        {
            get => _capsuleAngularSpeed * (float)Math.PI / 180;
            set => _capsuleAngularSpeed = value;
        }

        public float CapsuleDelay
        {
            get => _capsuleDelay;
            set => _capsuleDelay = value;
        }
    }
}
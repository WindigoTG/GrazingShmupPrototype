using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct ArcConfig
    {
        [Header("Arc settings")]
        [Min(1)] [Tooltip("Number of bullets in an arc")] public int ArcBulletCount;
        [SerializeField][Tooltip("An angle of an arc")] float _arcAngle;
        [Tooltip("Offset from the fireing position")] public float InitialRadius;
        [Tooltip("Makes bullets in left and right halves of an arc turn in opposite directions\n" +
                                    "relative to each other")] public bool SymmetrizeTurning;

        public float ArcAngle
        {
            get => _arcAngle * Mathf.PI / 180;
            set => _arcAngle = value * 180 / Mathf.PI;
        }
    }
}

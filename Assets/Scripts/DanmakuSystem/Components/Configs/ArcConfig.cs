using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct ArcConfig
    {
        #region Fields

        [Header("Arc settings")]
        [SerializeField] [Min(1)] [Tooltip("Number of bullets in an arc")] int _arcBulletCount;
        [Tooltip("An angle of an arc")] public float ArcAngle;
        [Tooltip("Offset from the fireing position")] public float InitialRadius;
        [Tooltip("Makes bullets in left and right halves of an arc turn in opposite directions\n" +
                                    "relative to each other")] public bool SymmetrizeTurning;

        #endregion


        #region Properties

        public int ArcBulletCount
        {
            get => _arcBulletCount;
            set => _arcBulletCount = value >= 1 ? value : 1;
        }

        #endregion
    }
}

using System;
using UnityEngine;

namespace GrazingShmup
{
    [Serializable]
    public struct RowConfig
    {
        #region Fields

        [Header("Row settings")]
        [Min(0)]
        [Tooltip("Number of additional bullets in an row" +
                                            "\n0 means only the default single bullet will be shot")]
        [SerializeField] private int _rowAdditionalBulletCount;

        [Tooltip("Horisontal offset of additional bullets")] public float RowHorizontalOffset;
        [Tooltip("Vertiacal offset of additional bullets")] public float RowVerticalOffset;
        [Tooltip("If true, additional bullets are added on both sides, with the same offset")] public bool IsTwoSided;
        [Tooltip("If true, additional bullets are added on both sides, in symmetrical fashion")] public bool IsMirrored;
        [Min(0)] [Tooltip("If mirrored, puts a gap between left and right sires")] public float RowGap;

        #endregion


        #region Properties

        public int RowAdditionalBulletCount
        {
            get => _rowAdditionalBulletCount;
            set => _rowAdditionalBulletCount = Math.Abs(value);
        }

        #endregion
    }
}

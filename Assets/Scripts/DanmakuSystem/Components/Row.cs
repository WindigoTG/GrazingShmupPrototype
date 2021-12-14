using UnityEngine;

namespace GrazingShmup
{
    public class Row : Projectile
    {
        #region IProjectile

        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            config.Position = position;
            config.Rotation = rotation;

            var angle = config.Rotation.z * Mathf.PI / 180;

            float gap = (config.RowSettings.IsTwoSided && config.RowSettings.IsMirrored && config.RowSettings.RowGap > 0) ? config.RowSettings.RowGap / 2 : 0;

            Vector3 gapOffset = new Vector3((0 * Mathf.Sin(angle) + gap * Mathf.Cos(angle)),
                        (gap * Mathf.Sin(angle) + 0 * Mathf.Cos(angle)),
                        0.0f);

            SubFire(config, position + gapOffset, rotation);

            if (config.RowSettings.IsTwoSided && config.RowSettings.IsMirrored && config.RowSettings.RowGap > 0)
                SubFire(config, position - gapOffset, rotation);

            if (config.RowSettings.RowAdditionalBulletCount > 0)
            {
                for (var i = 1; i <= config.RowSettings.RowAdditionalBulletCount; i++)
                {
                    

                    Vector3 offset = new Vector3((-config.RowSettings.RowVerticalOffset * Mathf.Sin(angle) + config.RowSettings.RowHorizontalOffset * Mathf.Cos(angle)) * i,
                        (config.RowSettings.RowHorizontalOffset * Mathf.Sin(angle) + config.RowSettings.RowVerticalOffset * Mathf.Cos(angle)) * i, 
                        0.0f);

                    SubFire(config, position + offset + gapOffset, rotation);

                    if (config.RowSettings.IsMirrored || config.RowSettings.IsTwoSided)
                    {
                        float yOffsetModifier = config.RowSettings.IsMirrored ? 1 : -1;

                        Vector3 offsetMirrored = new Vector3((-config.RowSettings.RowVerticalOffset * Mathf.Sin(angle) - config.RowSettings.RowHorizontalOffset * Mathf.Cos(angle)) * i,
                        (-config.RowSettings.RowHorizontalOffset * Mathf.Sin(angle) + (config.RowSettings.RowVerticalOffset * yOffsetModifier) * Mathf.Cos(angle)) * i,
                        0.0f);
                        SubFire(config, position + offsetMirrored - gapOffset, rotation);
                    }
                }
            }
        }

        #endregion
    }
}
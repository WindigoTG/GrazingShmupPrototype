using UnityEngine;

namespace GrazingShmup
{
    public class Row : Fireable
    {
        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            config.Position = position;
            config.Rotation = rotation;

            SubFire(config, position, rotation);

            if (config.RowLineCount > 1)
            {
                for (var i = 1; i < config.RowLineCount; i++)
                {
                    var angle = config.Rotation.z * Mathf.PI / 180;

                    Vector3 offset = new Vector3((-config.RowVerticalOffset * Mathf.Sin(angle) + config.RowLineOffset * Mathf.Cos(angle)) * i,
                        (config.RowLineOffset * Mathf.Sin(angle) + config.RowVerticalOffset * Mathf.Cos(angle)) * i, 
                        0.0f);

                    SubFire(config, position + offset, rotation);

                    if (config.IsMirrored)
                    {
                        Vector3 offsetMirrored = new Vector3((-config.RowVerticalOffset * Mathf.Sin(angle) - config.RowLineOffset * Mathf.Cos(angle)) * i,
                        (-config.RowLineOffset * Mathf.Sin(angle) + config.RowVerticalOffset * Mathf.Cos(angle)) * i,
                        0.0f);
                        SubFire(config, position + offsetMirrored, rotation);
                    }
                }
            }
        }
    }
}
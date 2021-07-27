using UnityEngine;

namespace GrazingShmup
{
    public class Arc : Fireable
    {
        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            config.Position = position;
            config.Rotation = rotation;

            if (config.ArcSettings.ArcBulletCount == 1)
            {
                SubFire(config, position, rotation);
                return;
            }

            var start = config.Rotation.z * Mathf.PI / 180 - config.ArcSettings.ArcAngle / 2;
            for (int i = 0; i < config.ArcSettings.ArcBulletCount; i++)
            {
                var angle = start + i * (config.ArcSettings.ArcAngle / (config.ArcSettings.ArcBulletCount - 1));
                var currentConfig = config;

                if (config.ArcSettings.SymmetrizeTurning)
                {
                    if (angle < 0)
                    {
                        currentConfig.ModifyTurning(-1);
                    }
                    if (angle == 0)
                    {
                        currentConfig.ModifyTurning(0);
                    }
                }

                currentConfig.Position = config.Position + (config.ArcSettings.InitialRadius * new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
                currentConfig.Rotation = (new Vector3(config.Rotation.x, config.Rotation.y, angle * 180 / Mathf.PI));

                SubFire(currentConfig, currentConfig.Position, currentConfig.Rotation);
            }
        }
    }
}
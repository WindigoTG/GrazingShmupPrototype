using UnityEngine;

namespace GrazingShmup
{
    public class Line : Projectile
    {
        public override void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation)
        {
            for (var i = 0; i < config.LineSettings.LineBulletCount; i++)
            {
                //config.ProjectileSpeed += config.LineSettings.DeltaSpeedInLine;
                config.ModifySpeed(config.LineSettings.DeltaSpeedInLine);
                SubFire(config, position, rotation);
            }
        }
    }
}
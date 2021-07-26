using UnityEngine;

namespace GrazingShmup
{
    public class Line : Fireable
    {
        public override void Fire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            for (var i = 0; i < config.LineBulletCount; i++)
            {
                config.BulletSpeed += config.LineDeltaSpeed;
                SubFire(config, position, rotation);
            }
        }
    }
}
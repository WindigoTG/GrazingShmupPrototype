using UnityEngine;

namespace GrazingShmup
{
    public class Arc : Fireable
    {
        public override void Fire(BullletConfig config, Vector3 position, Vector3 rotation)
        {
            config.Position = position;
            config.Rotation = rotation;

            if (config.ArcLineCount == 1)
            {
                SubFire(config, position, rotation);
                return;
            }

            var start = config.Rotation.z * Mathf.PI / 180 - config.ArcAngle / 2;
            for (int i = 0; i < config.ArcLineCount; i++)
            {
                var angle = start + i * (config.ArcAngle / (config.ArcLineCount - 1));

                var currentConfig = config;
                currentConfig.Position = config.Position + (config.InitialRadius * new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
                currentConfig.Rotation = (new Vector3(config.Rotation.x, config.Rotation.y, angle * 180 / Mathf.PI));

                SubFire(currentConfig, currentConfig.Position, currentConfig.Rotation);
            }
        }
    }
}
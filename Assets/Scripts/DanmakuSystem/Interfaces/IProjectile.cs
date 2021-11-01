using UnityEngine;

namespace GrazingShmup
{
    public interface IProjectile
    {
        public void Fire(BulletConfig config, Vector3 position, Vector3 rotation);
    }
}
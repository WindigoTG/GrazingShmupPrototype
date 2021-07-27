using UnityEngine;

namespace GrazingShmup
{
    public interface IFireable
    {
        public void Fire(ProjectileConfig config, Vector3 position, Vector3 rotation);
    }
}
using UnityEngine;

namespace GrazingShmup
{
    public abstract class Projectile : IProjectile
    {
        protected IProjectile _subProjectile;

        public abstract void Fire(BulletConfig config, Vector3 position, Vector3 rotation);

        protected void SubFire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            if (_subProjectile != null)
                _subProjectile.Fire(config, position, rotation);
        }

        public IProjectile SubFireable 
        { 
            set { _subProjectile = value; }
            get { return _subProjectile; }
        }
    }
}
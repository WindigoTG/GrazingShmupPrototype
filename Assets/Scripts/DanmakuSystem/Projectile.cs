using UnityEngine;

namespace GrazingShmup
{
    public abstract class Projectile : IProjectile
    {
        #region Fields

        protected IProjectile _subProjectile;

        #endregion


        #region Properties

        public IProjectile SubFireable
        {
            set { _subProjectile = value; }
            get { return _subProjectile; }
        }

        #endregion


        #region IProjectile

        public abstract void Fire(BulletConfig config, Vector3 position, Vector3 rotation);

        #endregion


        #region Methods

        protected void SubFire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            if (_subProjectile != null)
                _subProjectile.Fire(config, position, rotation);
        }

        #endregion
    }
}
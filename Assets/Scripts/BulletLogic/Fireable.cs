using UnityEngine;

namespace GrazingShmup
{
    public abstract class Fireable : IFireable
    {
        protected IFireable _subFireable;

        public abstract void Fire(BulletConfig config, Vector3 position, Vector3 rotation);

        protected void SubFire(BulletConfig config, Vector3 position, Vector3 rotation)
        {
            if (_subFireable != null)
                _subFireable.Fire(config, position, rotation);
        }

        public IFireable SubFireable 
        { 
            set { _subFireable = value; }
            get { return _subFireable; }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace GrazingShmup
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet")]
    public sealed class BulletData : ScriptableObject
    {
        public BulletConfig BulletConfig;
        public BulletBase BaseBullet;
        public BulletOwner BulletOwner;
        public bool IsTracking;

        public List<BulletComponent> BulletComponents;
    }
}
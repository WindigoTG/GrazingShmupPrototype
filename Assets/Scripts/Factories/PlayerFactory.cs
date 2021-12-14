using UnityEngine;

namespace GrazingShmup
{
    public class PlayerFactory : IPlayerFactory
    {
        #region Fields

        BulletData _bulletData;

        #endregion


        #region IPlayerFactory

        public PlayerShip CreatePlayer()
        {
            return new PlayerShip(Resources.Load<PlayerData>(References.Player_Data), new PlayerEngine(), 
                                                            new PlayerWeapon(LoadBulletConfig(), 
                                                            ServiceLocator.GetService<BulletFactory>().GetBullet(BulletBase.Bullet, _bulletData.BulletComponents, _bulletData.BulletOwner)));
        }

        #endregion


        #region Methods

        private BulletConfig[] LoadBulletConfig()
        {
            BulletConfig[] config = new BulletConfig[5];

            for (int i = 0; i < 5; i++)
            {
                _bulletData = Resources.Load<BulletData>(References.Player_Bullet_Data + i.ToString());
                config[i] = _bulletData.BulletConfig;
            }

            return config;
        }

        #endregion
    }
}
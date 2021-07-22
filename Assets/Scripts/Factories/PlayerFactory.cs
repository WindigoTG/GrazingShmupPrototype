using UnityEngine;

namespace GrazingShmup
{
    public class PlayerFactory : IPlayerFactory
    {
        BulletData _bulletData;

        public PlayerFactory()
        {
            
        }

        public PlayerShip CreatePlayer()
        {
            return new PlayerShip(Resources.Load<PlayerData>(References.Player_Data), new PlayerEngine(), new PlayerWeapon(LoadBulletConfig()));
        }

        private BulletConfig[] LoadBulletConfig()
        {
            BulletConfig[] config = new BulletConfig[5];

            for (int i = 0; i < 5; i++)
            {
                _bulletData = Resources.Load<BulletData>(References.Player_Bullet_Data + i.ToString());
                config[i] = _bulletData.GetConfig();
            }

            return config;
        }
    }
}
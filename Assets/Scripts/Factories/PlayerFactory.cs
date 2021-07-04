using UnityEngine;

namespace GrazingShmup
{
    public class PlayerFactory : IPlayerFactory
    {
        BulletData _bulletData;

        public PlayerFactory()
        {
            _bulletData = Resources.Load<BulletData>(ConstantsAndMagicLines.Player_Bullet_Data);
        }

        public PlayerShip CreatePlayer()
        {
            return new PlayerShip(Resources.Load<PlayerData>(ConstantsAndMagicLines.Player_Data), new PlayerEngine(), new PlayerWeapon(_bulletData.GetConfig()));
        }
    }
}
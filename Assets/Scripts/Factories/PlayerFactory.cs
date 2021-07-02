using UnityEngine;

namespace GrazingShmup
{
    public class PlayerFactory : IPlayerFactory
    {
        public PlayerShip CreatePlayer()
        {
            return new PlayerShip(Resources.Load<PlayerData>(ConstantsAndMagicLines.Player_Data), new PlayerEngine(), null);
        }
    }
}
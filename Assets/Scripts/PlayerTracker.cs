using UnityEngine;

namespace GrazingShmup
{
    public class PlayerTracker 
    {
        Transform _player;

        public PlayerTracker(Transform player)
        {
            _player = player;
        }

        public Transform Player => _player;

        public void UpdatePlayer(Transform player)
        {
            _player = player;
        }
    }
}
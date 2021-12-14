using UnityEngine;

namespace GrazingShmup
{
    public class PlayerTracker 
    {
        #region Fields

        Transform _player;

        #endregion


        #region Properties

        public Transform Player => _player;

        #endregion


        #region ClassLifeCycles

        public PlayerTracker(Transform player)
        {
            _player = player;
        }

        #endregion


        #region Methods

        public void UpdatePlayer(Transform player)
        {
            _player = player;
        }

        #endregion
    }
}
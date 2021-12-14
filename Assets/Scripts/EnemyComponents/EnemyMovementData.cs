using UnityEngine;

namespace GrazingShmup
{
    [CreateAssetMenu(fileName = "EnemyMovementData", menuName = "Data/EnemyMovement")]
    public class EnemyMovementData : ScriptableObject
    {
        #region Fields

        [SerializeField, Range(0, 10)] private float _speed;
        [SerializeField] private float _moveTreshold;

        #endregion


        #region Properties

        public float Speed => _speed;
        public float MoveTreshold => _moveTreshold;

        #endregion
    }
}
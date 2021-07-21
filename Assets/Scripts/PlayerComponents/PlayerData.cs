using UnityEngine;

namespace GrazingShmup
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public sealed class PlayerData : ScriptableObject
    {
        [SerializeField, Range(0, 10)] private float _speed;
        [SerializeField] private Vector3 _position;
        [SerializeField] private float _screenBoundMargin;
        [SerializeField] private GameObject _prefab;
        public float Speed => _speed;
        public Vector3 Position => _position;
        public float ScreenBoundMargin => _screenBoundMargin;
        public GameObject Prefab => _prefab;
    }
}
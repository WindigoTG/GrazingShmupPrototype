using System;
using UnityEngine;

namespace GrazingShmup
{
    public class CollisionManager
    {
        #region Fields

        RaycastHit2D[] _hits = new RaycastHit2D[128];

        Transform _player;
        float _playerHitPointRadius;
        (float x, float y) _playerGrazeColliderSize;

        public Action<Transform> EnemyHit;
        public Action PlayerHit;
        public Action<Vector3> PlayerGrazed;

        #endregion


        #region Methods

        public bool CheckCollisions(Vector3 origin, float radius, Vector3 direction, int layerMask)
        {
            if (layerMask == LayerMask.GetMask(References.PlayerHitBox))
            {
                if (CheckIfWithinGrazingDistance(origin, radius))
                    CheckGrazing(origin, radius, direction);

                if (CheckIfWithinCollidingDistance(origin, radius))
                    return CheckHitCollision(origin, radius, direction, layerMask);
                else
                    return false;
            }
            else
            {
                return CheckHitCollision(origin, radius, direction, layerMask);
            }
        }

        private bool CheckIfWithinCollidingDistance(Vector3 origin, float radius)
        {
            float sqrDistanceBetweenColliders = (origin - _player.position).sqrMagnitude;
            float sqrSumOfCollidersRadii = (radius + _playerHitPointRadius) * (radius + _playerHitPointRadius);

            return sqrDistanceBetweenColliders <= sqrSumOfCollidersRadii;
        }

        private bool CheckIfWithinGrazingDistance(Vector3 origin, float radius)
        {
            float horizontalDistanceBetweenColliders = Mathf.Abs(origin.x - _player.position.x);
            float verticalDistanceBetweenColliders = Mathf.Abs(origin.y - _player.position.y);

            float horisontalCollirersSizeSum = _playerGrazeColliderSize.x + radius;
            float verticalCollirersSizeSum = _playerGrazeColliderSize.y + radius;

            return (horizontalDistanceBetweenColliders <= horisontalCollirersSizeSum) || 
                   (verticalDistanceBetweenColliders <= verticalCollirersSizeSum);
        }

        private void CheckGrazing(Vector3 origin, float radius, Vector3 direction)
        {
            float maxDistance = direction.magnitude;

            Array.Clear(_hits, 0, _hits.Length);
            if (Physics2D.CircleCastNonAlloc(origin, radius, direction.normalized, _hits, maxDistance, LayerMask.GetMask(References.PlayerGraze)) > 0)
            {
                foreach (var h in _hits)
                    if (h.collider != null)
                    {
                        NotifyObservers(h, LayerMask.GetMask(References.PlayerGraze));
                        break;
                    }
            }
        }

        private bool CheckHitCollision(Vector3 origin, float radius, Vector3 direction, int layerMask)
        {
            float maxDistance = direction.magnitude;

            Array.Clear(_hits, 0, _hits.Length);

            if (Physics2D.CircleCastNonAlloc(origin, radius, direction.normalized, _hits, maxDistance, layerMask) > 0)
            {
                foreach (var h in _hits)
                    if (h.collider != null)
                    {
                        NotifyObservers(h, layerMask);
                        return true;
                    }
            }
            return false;
        }

        void NotifyObservers(RaycastHit2D hit, int layerMask)
        {
            if (layerMask == LayerMask.GetMask(References.PlayerGraze))
            {
                PlayerGrazed?.Invoke(hit.point);
            }
            if (layerMask == LayerMask.GetMask(References.PlayerHitBox))
            {
                PlayerHit?.Invoke();
            }
            if (layerMask == LayerMask.GetMask(References.EnemyLayer))
            {
                EnemyHit?.Invoke(hit.collider.transform);
            }
        }

        public void RegisterPlayer(Transform player, float hitPointRadius, (float x, float y) grazeColliderSize)
        {
            _player = player;
            _playerHitPointRadius = hitPointRadius;
            _playerGrazeColliderSize = grazeColliderSize;
        }

        #endregion
    }
}
using System;
using UnityEngine;

namespace GrazingShmup
{
    public class CollisionManager
    {
        RaycastHit2D[] _hits = new RaycastHit2D[128];

        Transform _player;

        public Action<Transform> EnemyHit;
        public Action PlayerHit;
        public Action<Vector3> PlayerGrazed;

        public bool CheckCollisions(Vector3 origin, float radius, Vector3 direction, int layerMask)
        {
            float maxDistance = direction.magnitude;

            if (layerMask == LayerMask.GetMask(ConstantsAndMagicLines.PlayerHitBox))
            {
                Array.Clear(_hits, 0, _hits.Length);
                if (Physics2D.CircleCastNonAlloc(origin, radius, direction.normalized, _hits, maxDistance, LayerMask.GetMask(ConstantsAndMagicLines.PlayerGraze)) > 0)
            {
                    foreach (var h in _hits)
                        if (h.collider != null)
                        {
                            NotifyObservers(h, LayerMask.GetMask(ConstantsAndMagicLines.PlayerGraze));
                            break;
                        }
                }
            }

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
            if (layerMask == LayerMask.GetMask(ConstantsAndMagicLines.PlayerGraze))
            {
                PlayerGrazed?.Invoke(hit.point);
            }
            if (layerMask == LayerMask.GetMask(ConstantsAndMagicLines.PlayerHitBox))
            {
                PlayerHit?.Invoke();
            }
            if (layerMask == LayerMask.GetMask(ConstantsAndMagicLines.EnemyLayer))
            {
                EnemyHit?.Invoke(hit.collider.transform);
            }
        }

        public void RegisterPlayer(Transform player)
        {
            _player = player;
        }
    }
}
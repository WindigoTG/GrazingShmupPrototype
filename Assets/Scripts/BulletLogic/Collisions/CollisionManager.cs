using System;
using UnityEngine;

namespace GrazingShmup
{
    public class CollisionManager
    {
        RaycastHit2D[] _hits = new RaycastHit2D[128];

        public Action<Transform> EnemyHit;
        public Action PlayerHit;

        public bool CheckCollisions(Vector3 origin, float radius, Vector3 direction, int layerMask)
        {

            Array.Clear(_hits, 0, _hits.Length);

            float maxDistance = direction.magnitude;
            
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
            if (layerMask == LayerMask.GetMask(ConstantsAndMagicLines.PlayerLayer))
            {
                PlayerHit?.Invoke();
            }
            if (layerMask == LayerMask.GetMask(ConstantsAndMagicLines.EnemyLayer))
            {
                EnemyHit?.Invoke(hit.collider.transform);
            }
        }
    }
}
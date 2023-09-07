using UnityEngine;

namespace Projectiles
{
    public class CannonProjectile : Projectile
    {
        public void Init(Vector3 direction)
        {
            Direction = direction.normalized;
        }
    }
}
using UnityEngine;

namespace Projectiles
{
    public class CannonProjectile : Projectile
    {
        public void Init(Vector3 direction, float speed)
        {
            Rigidbody.velocity = direction * speed;
        }
    }
}
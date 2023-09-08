using System;
using Factory;
using Monster;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : PoolObject
    {
        [FormerlySerializedAs("m_damage")] [SerializeField] private int _damage = 1;

        private Rigidbody _rigidbody;
        protected Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        private void OnTriggerEnter(Collider other)
        {
            if ( ! other.TryGetComponent(out MonsterVitals monster))
                return;

            monster.TakeDamage(_damage);
            PoolDisable();
        }
    }
}
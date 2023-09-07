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
        [FormerlySerializedAs("m_speed")] [SerializeField] private float _speed = 0.2f;

        private Rigidbody _rigidbody;
        private Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        protected Vector3 Direction
        {
            get => Rigidbody.velocity.normalized;
            set => Rigidbody.velocity = _speed * value.normalized;
        }

        private void OnTriggerEnter(Collider other)
        {
            print(other);
            if ( ! other.TryGetComponent(out MonsterVitals monster))
                return;

            monster.TakeDamage(_damage);
            PoolDisable();
        }
    }
}
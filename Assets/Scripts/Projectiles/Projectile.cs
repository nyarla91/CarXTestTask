using System.Collections;
using Factory;
using Monster;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : PoolObject
    {
        [SerializeField] private float _lifespan;
        [SerializeField] private int _damage = 1;

        private Coroutine _lifespanCoroutine;
        
        private Rigidbody _rigidbody;
        protected Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        public override void OnPoolEnable()
        {
            base.OnPoolEnable();
            _lifespanCoroutine = StartCoroutine(LifespanProgress());
        }

        public override void PoolDisable()
        {
            base.PoolDisable();
            if (_lifespanCoroutine != null)
                StopCoroutine(_lifespanCoroutine);
        }

        private IEnumerator LifespanProgress()
        {
            yield return new WaitForSeconds(_lifespan);
            PoolDisable();
        }

        private void OnTriggerEnter(Collider other)
        {
            if ( ! other.TryGetComponent(out MonsterVitals monster))
                return;

            monster.TakeDamage(_damage);
            PoolDisable();
        }
    }
}
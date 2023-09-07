using System.Collections;
using Factory;
using Projectiles;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
public abstract class Tower<TProjectile> : Transformable where TProjectile : Projectile
{
    [SerializeField] private float _shootInterval = 0.5f;

    private PoolFactory _projectileFactory;
    private PoolFactory ProjectileFactory => _projectileFactory ??= GetComponent<PoolFactory>();

    protected abstract Transform Target { get; }

    private void Start()
    {
        StartCoroutine(ShootCycle());
    }

    private IEnumerator ShootCycle()
    {
        while (true)
        {
            yield return new WaitUntil(() => Target != null);

            TProjectile projectile = ProjectileFactory.GetNewObject<TProjectile>(Transform.position);
            InitProjectile(projectile, Target);

            yield return new WaitForSeconds(_shootInterval);
        }
    }

    protected abstract void InitProjectile(TProjectile projectile, Transform target);
}
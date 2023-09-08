using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Factory;
using Monster;
using Projectiles;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
public abstract class Tower<TProjectile> : Transformable where TProjectile : Projectile
{
    [SerializeField] private float _shootInterval = 0.5f;
    [SerializeField] private float _range = 4;
    [SerializeField] private Transform _shootOrigin;

    protected Transform ShootOrigin => _shootOrigin;

    protected ITowerTarget Target
    {
        get
        {
            IEnumerable<Collider> collidersInRange = Physics.OverlapSphere(Transform.position, _range);
            IEnumerable<ITowerTarget> targetsInRange = collidersInRange.Select(collider => collider.GetComponent<ITowerTarget>());
            targetsInRange = targetsInRange.Where(target => target != null).Where(target => target.Alive);
            return targetsInRange.MinElement(target => Vector3.Distance(Transform.position, target.CurrentPosition));
        }
    }
    
    private PoolFactory _projectileFactory;
    private PoolFactory ProjectileFactory => _projectileFactory ??= GetComponent<PoolFactory>();

    private void Start()
    {
        StartCoroutine(ShootCycle());
    }

    private IEnumerator ShootCycle()
    {
        while (true)
        {
            yield return new WaitUntil(() => Target != null);

            TProjectile projectile = ProjectileFactory.GetNewObject<TProjectile>(_shootOrigin.position);
            InitProjectile(projectile, Target);

            yield return new WaitForSeconds(_shootInterval);
        }
    }

    protected abstract void InitProjectile(TProjectile projectile, ITowerTarget target);
}
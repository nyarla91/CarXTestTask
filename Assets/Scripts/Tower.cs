using System;
using System.Collections;
using UnityEngine;

public abstract class Tower<TProjectile> : Transformable where TProjectile : Projectile {
    [SerializeField] private float m_shootInterval = 0.5f;
    [SerializeField] private float m_range = 4f;
    [SerializeField] private GameObject m_projectilePrefab;

    protected abstract Transform MTarget { get; }
    
    private void Start()
    {
        StartCoroutine(ShootCycle());
    }

    private IEnumerator ShootCycle()
    {
        while (true)
        {
            yield return new WaitUntil(() => MTarget != null);

            if (m_projectilePrefab == null)
                throw new UnassignedReferenceException($"{gameObject} projectile prefab is unassigned");
            TProjectile projectile = Instantiate(m_projectilePrefab, Transform.position, Quaternion.identity).GetComponent<TProjectile>();
            InitProjectile(projectile, MTarget);
            
            yield return new WaitForSeconds(m_shootInterval);
        }
    }

    protected abstract void InitProjectile(TProjectile projectile, Transform target);
}
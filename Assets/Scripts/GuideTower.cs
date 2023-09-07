using System.Collections.Generic;
using System.Linq;
using Monster;
using Projectiles;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class GuideTower : Tower<GuidedProjectile>
{
	[SerializeField] private float _range = 4;

	protected override Transform Target
	{
		get
		{
			IEnumerable<Collider> collidersInRange = Physics.OverlapSphere(Transform.position, _range);
			collidersInRange = collidersInRange.Where(collider => collider.GetComponent<MonsterVitals>() != null);
			IEnumerable<Transform> targetsInRange = collidersInRange.Select(collider => collider.transform);
			return targetsInRange.MinElement(target => Vector3.Distance(Transform.position, target.position));
		}
	}

	protected override void InitProjectile(GuidedProjectile projectile, Transform target)
	{
		projectile.Init(target);
	}
}
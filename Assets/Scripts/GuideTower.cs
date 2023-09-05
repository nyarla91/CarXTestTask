using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuideTower : Tower<GuidedProjectile> {
	[SerializeField] private float m_range;
	
	protected override Transform MTarget {
		get
		{
			IEnumerable<Collider> collidersInRange = Physics.OverlapSphere(Transform.position, m_range);
			collidersInRange = collidersInRange.Where(collider => collider.GetComponent<Monster>() != null);
			var targetsInRange = collidersInRange.Select(collider => collider.transform);
			return targetsInRange.MinElement(target => Vector3.Distance(Transform.position, target.position));
		}
	}
	
	protected override void InitProjectile(GuidedProjectile projectile, Transform target) {
		var projectileBeh = projectile.GetComponent<GuidedProjectile>();
		projectileBeh.Init(MTarget);
	}
}

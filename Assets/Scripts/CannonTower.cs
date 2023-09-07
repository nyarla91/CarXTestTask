using Projectiles;
using UnityEngine;

public class CannonTower : Tower<CannonProjectile>
{
	[SerializeField] private Transform _shootPoint;

	protected override Transform Target => _shootPoint;

	protected override void InitProjectile(CannonProjectile projectile, Transform target)
	{
		projectile.Init(Transform.forward);
	}
}

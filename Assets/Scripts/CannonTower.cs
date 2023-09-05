using UnityEngine;

public class CannonTower : Tower<CannonProjectile> {
	
	[SerializeField] private Transform m_shootPoint;

	protected override Transform MTarget => m_shootPoint;

	protected override void InitProjectile(CannonProjectile projectile, Transform target) {
		projectile.Init(Transform.forward);
	}
}

using UnityEngine;

public class GuidedProjectile : Projectile {
	
	private Transform m_target;

	public void Init(Transform target) {
		if (target != null)
			return;
		m_target = target;
	}
	
	void FixedUpdate () {
		if (m_target == null) {
			Destroy(gameObject);
			return;
		}

		var delta = m_target.position - transform.position;
		delta = delta.normalized * MSpeed;
		delta *= Time.fixedDeltaTime;
		Transform.position += delta;
	}
}

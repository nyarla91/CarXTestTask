using UnityEngine;

namespace Projectiles
{
	public class GuidedProjectile : Projectile
	{
		private Transform _target;

		public void Init(Transform target)
		{
			if (_target != null)
				return;
			_target = target;
		}

		public override void PoolDisable()
		{
			base.PoolDisable();
			_target = null;
		}

		private void FixedUpdate()
		{
			if (_target == null)
			{
				PoolDisable();
				return;
			}

			Direction = _target.position - Transform.position;
		}
	}
}
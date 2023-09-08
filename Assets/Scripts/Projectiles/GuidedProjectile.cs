using Monster;
using UnityEngine;

namespace Projectiles
{
	public class GuidedProjectile : Projectile
	{
		[SerializeField] private float _speed = 0.2f;
		
		private ITowerTarget _target;

		public void Init(ITowerTarget target)
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
			if (_target == null || !_target.Alive)
			{
				PoolDisable();
				return;
			}
			
			Vector3 direction = _target.CurrentPosition - Transform.position;
			direction.Normalize();
			Rigidbody.velocity = direction * _speed;
		}
	}
}
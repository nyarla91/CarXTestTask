using Factory;
using UnityEngine;

namespace Monster
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(PoolObject))]
	public class MonsterMovement : Transformable
	{
		[SerializeField] private float _speed = 0.1f;

		private Transform _moveTarget;

		private Rigidbody _rigidbody;
		private PoolObject _poolObject;

		private Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();
		private PoolObject PoolObject => _poolObject ??= GetComponent<PoolObject>(); 


		public void Init(Transform moveTarget) {
			if (_moveTarget != null)
				return;
			_moveTarget = moveTarget;
		}

		void FixedUpdate ()
		{
			Move();
		}

		private void Move()
		{
			if (_moveTarget == null)
				return;
			
			Vector3 direction = _moveTarget.position - Transform.position;
			Rigidbody.velocity = direction * _speed;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.transform.Equals(_moveTarget))
				PoolObject.PoolDisable();
		}
	}
}

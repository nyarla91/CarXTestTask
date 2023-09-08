using Factory;
using UnityEngine;

namespace Monster
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(PoolObject))]
	public class MonsterMovement : Transformable, ITowerTarget
	{
		[SerializeField] private float _speed = 0.1f;

		private Transform _moveTarget;

		private Rigidbody _rigidbody;
		private PoolObject _poolObject;

		public bool Alive => gameObject.activeSelf;
		public Vector3 CurrentPosition => Transform.position;

		private Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();
		private PoolObject PoolObject => _poolObject ??= GetComponent<PoolObject>(); 


		public void Init(Transform moveTarget) {
			if (_moveTarget != null)
				return;
			_moveTarget = moveTarget;
		}


		public Vector3 PredictPosition(float timeAfter)
		{
			return Transform.position + Rigidbody.velocity * timeAfter;
		}

		void FixedUpdate ()
		{
			Move();
		}

		private void Move()
		{
			if (_moveTarget == null)
				return;
			
			Vector3 direction = (_moveTarget.position - Transform.position).normalized;
			Rigidbody.velocity = direction * _speed;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.transform.Equals(_moveTarget))
				PoolObject.PoolDisable();
		}
	}
}

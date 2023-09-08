using Monster;
using Projectiles;
using UnityEngine;

public class CannonTower : Tower<CannonProjectile>
{
	[SerializeField] [Tooltip("Degrees per second")] private float _turnSpeed;
	[SerializeField] private Transform _cannon;
	[SerializeField] [Range(0, 80)] private float _cannonAngle;
	[SerializeField] [Range(1, 5)] private int _predictIterations;
	
	private float _estimatedProjectileSpeed;
	private float _estimatedProjectileFlightTime;
	
	protected override void InitProjectile(CannonProjectile projectile, ITowerTarget target)
	{
		projectile.Init(_cannon.forward, _estimatedProjectileSpeed);
	}

	private void FixedUpdate()
	{
		if (Target == null)
			return;
		CalculateProjectileFlightTime();
		Vector3 forwardToTarget = (Target.PredictPosition(_estimatedProjectileFlightTime) - Transform.position).WithY(0).normalized;
		Quaternion targetRotation = Quaternion.LookRotation(forwardToTarget, Vector3.up);
		Transform.rotation = Quaternion.RotateTowards(Transform.rotation, targetRotation, _turnSpeed * Time.fixedDeltaTime);
	}

	private void CalculateProjectileFlightTime()
	{
		Vector3 targetPosition = Target.CurrentPosition;
		float height = Mathf.Abs((targetPosition - ShootOrigin.position).y);
		float gravity = Mathf.Abs(Physics.gravity.y);
		float verticalSpeed;

		_estimatedProjectileFlightTime = 0;
		for (int i = 0; i < _predictIterations; i++)
		{
			_estimatedProjectileSpeed = CalculateSpeedForProjectile(targetPosition);
			verticalSpeed = Mathf.Sin(_cannonAngle * Mathf.Deg2Rad) * _estimatedProjectileSpeed;
			_estimatedProjectileFlightTime = CalculateFallTime(verticalSpeed, gravity, height);
			targetPosition = Target.PredictPosition(_estimatedProjectileFlightTime);
		}
	}

	private float CalculateFallTime(float verticalSpeed, float gravity, float height)
	{
		float ascentTime = verticalSpeed / gravity;
		float ascentHeight = Mathf.Pow(verticalSpeed, 2) / (2 * gravity); // Формула пути, равнозамедленного движения
		float descentTime = Mathf.Sqrt(2 * (height + ascentHeight) / gravity); // Формула времени равноускоренного движения
		return ascentTime + descentTime;
	}

	private float CalculateSpeedForProjectile(Vector3 targetPosition)
	{
		Vector3 delta = targetPosition - ShootOrigin.position;
		Vector3 deltaProjection = delta.WithY(0);
		float x = deltaProjection.magnitude;
		float y = delta.y;
		float a = _cannonAngle * Mathf.Deg2Rad;
		float g = -Physics.gravity.y;

		// Формула: https://youtu.be/lXSzdGBIPkg?si=T3SA7RR41LtV0Nxe&t=613
		float vSquared = (g * x * x) / (2 * (y - Mathf.Tan(a) * x) * Mathf.Pow(Mathf.Cos(a), 2));
		return Mathf.Sqrt(Mathf.Abs(vSquared));
	}

	private void OnValidate()
	{
		_cannon.localRotation = Quaternion.Euler(-_cannonAngle, 0, 0);
	}
}

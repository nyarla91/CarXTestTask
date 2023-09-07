using UnityEngine;
using System.Collections;
using Factory;
using Monster;

[RequireComponent(typeof(PoolFactory))]
public class Spawner : Transformable
{
	[SerializeField] private float _firstSpawnDelay = 1;
	[SerializeField] private float _interval = 3;
	[SerializeField] private Transform _moveTarget;

	private PoolFactory _monsterFactory;
	private PoolFactory MonsterFactory => _monsterFactory ??= GetComponent<PoolFactory>(); 
	
	private void Start()
	{
		StartCoroutine(SpawnCycle());
	}

	private IEnumerator SpawnCycle()
	{
		yield return new WaitForSeconds(_firstSpawnDelay);

		while (true)
		{
			MonsterMovement monster = MonsterFactory.GetNewObject<MonsterMovement>(Transform.position);
			monster.Init(_moveTarget);

			yield return new WaitForSeconds(_interval);
		}
	}
}
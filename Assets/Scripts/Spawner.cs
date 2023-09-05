using UnityEngine;
using System.Collections;

public class Spawner : Transformable {
	[SerializeField] private GameObject m_monsterPrefab;
	[SerializeField] private float m_firstSpawnDelay = 1;
	[SerializeField] private float m_interval = 3;
	[SerializeField] private Transform m_moveTarget;

	private void Start() {
		StartCoroutine(SpawnCycle());
	}

	private IEnumerator SpawnCycle() {
		yield return new WaitForSeconds(m_firstSpawnDelay);
		
		while (true) {
			var monster = Instantiate(m_monsterPrefab, Transform.position, Quaternion.identity).GetComponent<Monster>();
			monster.Init(m_moveTarget);
			
			yield return new WaitForSeconds(m_interval);
		}
	}
}

using UnityEngine;

public class Monster : MonoBehaviour {
	private const float m_reachDistance = 0.3f;
	
	[SerializeField] private float m_speed = 0.1f;
	[SerializeField] private int m_maxHP = 30;

	private int m_hp;
	private Transform m_moveTarget;

	void Start() {
		m_hp = m_maxHP;
	}

	public void TakeDamage(int damage) {
		if (damage < 0)
			return;
		m_hp -= damage;
	}

	public void Init(Transform moveTarget) {
		if (m_moveTarget != null)
			return;
		m_moveTarget = moveTarget;
	}

	void Update () {
		if (m_moveTarget == null)
			return;
		
		if (Vector3.Distance (transform.position, m_moveTarget.transform.position) <= m_reachDistance) {
			Destroy (gameObject);
			return;
		}

		var translation = m_moveTarget.transform.position - transform.position;
		if (translation.magnitude > m_speed) {
			translation = translation.normalized * m_speed;
		}
		transform.Translate (translation);
	}
}

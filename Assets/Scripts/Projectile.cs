using UnityEngine;

public class Projectile : Transformable {
    [SerializeField] private int m_damage;
    [SerializeField] private float m_speed = 0.2f;

    protected float MSpeed => m_speed;
    
    void OnTriggerEnter(Collider other) {
        if ( ! other.TryGetComponent(out Monster monster))
            return;

        monster.TakeDamage(m_damage);
        Destroy (gameObject);
    }
}
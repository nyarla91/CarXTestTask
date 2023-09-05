using UnityEngine;

public class CannonProjectile : Projectile {

    private Vector3 m_direction;
    
    public void Init(Vector3 direction) {
        if ( ! m_direction.Equals(Vector3.zero))
            return;
        
        m_direction = direction.normalized;
    }
    
    void FixedUpdate() {
        var delta = m_direction * MSpeed;
        Transform.position += delta;
    }
}
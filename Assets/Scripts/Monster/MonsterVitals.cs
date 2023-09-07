using Factory;
using UnityEngine;

namespace Monster
{
    [RequireComponent(typeof(PoolObject))]
    public class MonsterVitals : MonoBehaviour
    {
        [SerializeField] private int _maxHP = 30;
        
        private int _hp;

        private PoolObject _poolObject;
        
        private PoolObject PoolObject => _poolObject ??= GetComponent<PoolObject>(); 
        
        void Start()
        {
            _hp = _maxHP;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                return;
            _hp -= damage;
            if (_hp <= 0)
                PoolObject.PoolDisable();
        }
    }
}
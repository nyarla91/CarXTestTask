using UnityEngine;

namespace Monster
{
    public class MonsterView : MonoBehaviour
    {
        [SerializeField] private MonsterMovement _movement;
        [SerializeField] private Animator _animator;

        private void Update()
        {
            _animator.SetFloat("speed", _movement.Speed);
        }
    }
}
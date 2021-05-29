using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Weapon))]
    public class MediumTank : Enemy
    {
        [Header("Set in Inspector: Medium Tank")]
        public GameObject muzzle;
        
        [Header("Set options: Medium Tank")]
        public WeaponType type = WeaponType.Shell;
        /// <summary>
        /// Уменьшить или увеличить стандартный урон.  
        /// </summary>
        public float multiplicationDamage = 0.5f;
        /// <summary>
        /// Расстояние на котором враг прекращает движение.  
        /// </summary>
        [SerializeField] private float _attackRange = 10f;

        public delegate void WeaponFireDelegate(); 
        public WeaponFireDelegate FireDelegateEnemy;
        

        private bool _playerInAttackRange;

        protected override void Awake()
        {
            GetComponent<SphereCollider>().radius = _attackRange;
            
            base.Awake();
        }

        protected override void Update()
        {
            FireDelegateEnemy?.Invoke();
            
            if (_playerInAttackRange)
            {
                Stay();
            }
            else
            {
                ChasePlayer();
            }
        }
        
        private void OnTriggerEnter(Collider col)
        {
            if (!col.CompareTag("Player")) return;
            _playerInAttackRange = true;
        }

        private void OnTriggerExit(Collider col)
        {
            if (!col.CompareTag("Player")) return;
            _playerInAttackRange = false;
        }
        
        private void Stay()
        {
            agent.SetDestination(transform.position);
            
            transform.LookAt(player);
        }
    }
}

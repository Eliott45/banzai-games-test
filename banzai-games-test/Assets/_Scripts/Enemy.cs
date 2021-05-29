using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [Header("Set in Inspector: Enemy")]
        public float damageGive = 10f; // Нанесение урона игроку при столкновении 
        [SerializeField] private float _damageTake = 20f; // Получение урона при столкновении с игроком
        [SerializeField] private float _health = 100f;
        [Range(0, 1f)][SerializeField] private float _armor;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _showDamageDuration = 0.35f;

        [Header("Set Dynamically: Enemy")]
        public NavMeshAgent agent;
        public Transform player;
        
        private Color[] _originalColors;
        private Material[] _materials; // Все материалы игрового объекта и его потомков
        private bool _showingDamage;
        private float _damageDoneTime; //  Время прекращения отображения эффекта

        protected virtual void Awake()
        {
            _materials = Utils.GetAllMaterials(gameObject);
            _originalColors = new Color[_materials.Length];
            for (var i = 0; i < _materials.Length; i++){
                _originalColors[i] = _materials[i].color;
            }

            agent = GetComponent<NavMeshAgent>();
            agent.speed = _speed;
            player = GameObject.Find("Player").transform;
        }

        protected virtual void Update()
        {
            ChasePlayer();
        }

        protected void FixedUpdate()
        {
            if (_showingDamage && Time.time > _damageDoneTime) UnShowDamage();
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherGO = other.gameObject;
            switch (otherGO.tag)
            {
                case "Projectile":
                    var p = otherGO.GetComponent<Projectile>();
                    TakeDamage(Main.GetWeaponDefinition(p.Type).damageOnHit);
                    break;
                case "Player":
                    TakeDamage(_damageTake);
                    break;
            }
        }

        /// <summary>
        /// Преследовать игрока.
        /// </summary>
        protected void ChasePlayer()
        {
            agent.SetDestination(player.position);
        }

        /// <summary>
        /// Получить урон.
        /// </summary>
        /// <param name="damage">Входящий урон.</param>
        private void TakeDamage(float damage)
        {
            ShowDamage();
            if ((_health -= damage * (1f - _armor)) <= 0) Die();
        }
        
        /// <summary>
        /// Отобразить урон.
        /// </summary>
        private void ShowDamage() {
            foreach (var m in _materials) {
                m.color = Color.red;
            }
            _showingDamage = true;
            _damageDoneTime = Time.time + _showDamageDuration;
        }
        
        /// <summary>
        /// Прекратить отображать урон.
        /// </summary>
        private void UnShowDamage() {
            for (var i = 0; i < _materials.Length; i++) {
                _materials[i].color = _originalColors[i];
            }
            _showingDamage = false;
        }
    
        /// <summary>
        /// Умереть.
        /// </summary>
        private void Die()
        {
            Destroy(gameObject);
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace _Scripts
{
    /// <summary>
    /// Это перечисление всех возможных типов оружия танка.
    /// </summary>
    public enum WeaponType {
        /// <summary>
        ///  Простой снаряд танка.
        /// </summary>
        Shell, 
        /// <summary>
        /// Пулемет выпускает 3 снаряда одновременно. 
        /// </summary>
        MachineGun, 
        /// <summary>
        /// Более мощный снаряд танка.
        /// </summary>
        Rocket,
    }
    
    /// <summary>
    /// Класс WeaponDefinition позволяет настраивать свойства конкретного вида оружия. 
    /// </summary>
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class WeaponDefinition
    {
        public WeaponType type = WeaponType.Shell; // Тип оружия
        public GameObject projectilePrefab; // Префаб снаряда
        public float damageOnHit = 50f; // Урон от попадания
        public float delayBetweenShots; // КД следующего выстрела
        public float velocity = 20f; // Скорость снаряда
        public Color colorLight; // Цвет света от снаряда
    }

    public class Weapon : MonoBehaviour
    {
        private static Transform _projectileAnchor; // Пустой объект для корректного отображения снарядов иерархии 
        private PlayerShotController _playerShotController;
        private Enemy _enemyShotController;

        private float _lastShotTime; // Время последнего выстрела
        private WeaponDefinition _def; 
        private GameObject _muzzle; // Дуло
        
        private void Start()
        {
            // Динамически создать точку привязки для всех снарядов
            if(_projectileAnchor == null) {
                var go = new GameObject("_ProjectileAnchor");
                _projectileAnchor = go.transform;
            }  
            
            // Получить тип установленного оружия
            _playerShotController = gameObject.GetComponent<PlayerShotController>();
            _enemyShotController = gameObject.GetComponent<Enemy>();
            
            if (_playerShotController != null)
            {
                SetType(_playerShotController.type, _playerShotController.muzzle);
                _playerShotController.FireDelegate += Fire;
            }

            if (_enemyShotController == null) return;
            SetType(_enemyShotController.type, _enemyShotController.muzzle);
            _enemyShotController.FireDelegateEnemy += Fire;
        }

        private static WeaponType Type { get; set; }

        public void SetType(WeaponType wt, GameObject muzzle) {
            Type = wt;
            _def = Main.GetWeaponDefinition(Type);
            // _muzzle = _shotController.muzzle[(int)wt]; // Получить позицию дула для последующих выстрелов 
            _muzzle = muzzle;
            _lastShotTime = 0; // Сбросить КД выстрела 
        }
        
        /// <summary>
        /// Осуществляет выстрел.
        /// </summary>
        private void Fire()
        {
            if(Time.time - _lastShotTime < _def.delayBetweenShots) return; // Если КД еще не прошло 
             
            Projectile p;
            
            var vel = transform.forward * _def.velocity;
            
            switch (_def.type)
            {
                case WeaponType.Shell:
                    p = MakeProjectile();
                    p.rigid.velocity = vel;
                    break;
                case WeaponType.MachineGun:
                    // Снаряд летящий прямо
                    p = MakeProjectile();
                    p.rigid.velocity = vel;
                    // Снаряд летящий вправо
                    p = MakeProjectile();
                    Transform transform1;
                    (transform1 = p.transform).Rotate(0f,45f,0);
                    p.rigid.velocity = transform1.forward * _def.velocity;  
                    // Снаряд летящий влево
                    p = MakeProjectile();
                    (transform1 = p.transform).Rotate(0f,-45f,0);
                    p.rigid.velocity = transform1.forward * _def.velocity;  
                    break;
                case WeaponType.Rocket:
                    p = MakeProjectile();
                    p.rigid.velocity = vel;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Projectile MakeProjectile()
        {
            var go = Instantiate(_def.projectilePrefab, _projectileAnchor, true);
            
            go.tag = "Projectile";
            go.GetComponent<Light>().color = _def.colorLight;
            go.transform.position = _muzzle.transform.position;
            go.transform.rotation = _muzzle.transform.rotation;
            
            var p = go.GetComponent<Projectile>();
            
            p.Type = _def.type;
            _lastShotTime = Time.time;
            return(p);
        }
        
    }
}

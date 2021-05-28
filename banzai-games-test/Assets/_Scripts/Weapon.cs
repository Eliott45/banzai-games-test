using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace _Scripts
{
    /// <summary>
    /// Это перечисление всех возможных типов оружия танка.
    /// </summary>
    public enum WeaponType {
        Shell, // Простой снаряд танка
        Rocket,
        Mine,
        MachineGun
    }
    
    /// <summary>
    /// Класс WeaponDefinition позволяет настраивать свойства конкретного вида оружия. 
    /// </summary>
    [System.Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class WeaponDefinition
    {
        public WeaponType type = WeaponType.Shell; // Тип оружия
        public GameObject projectilePrefab; // Префаб снаряда
        public float damageOnHit = 50f; // Урон от попадания
        public float delayBetweenShots = 0f; // КД следующего выстрела
        public float velocity = 20f; // Скорость снаряда
    }

    public class Weapon : MonoBehaviour
    {
        private static WeaponType _type; // Текущий тип оружия 
        
        private static Transform _projectileAnchor; // Пустой объект для корректного отображения снарядов иерархии 
        private PlayerShotController _shotController; 
        
        public float lastShotTime; // Время последнего выстрела
        
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
            _shotController = gameObject.GetComponent<PlayerShotController>();
            if (_shotController == null) return;
            SetType(_shotController.type);
            _shotController.FireDelegate += Fire;
        }

        private WeaponType Type {
            get => (_type);
            set => SetType(value);
        }

        public void SetType(WeaponType wt) {
            _type = wt;
            _def = Main.GetWeaponDefinition(_type);
            _muzzle = _shotController.muzzles[(int)wt]; // Получить позицию дула для последующих выстрелов 
        }
        
        /// <summary>
        /// Осуществляет выстрел.
        /// </summary>
        private void Fire()
        {
            if(Time.time - lastShotTime < _def.delayBetweenShots) return; // Если КД еще не прошло 
             
            Projectile p;
            
            var vel = transform.forward * _def.velocity;
            
            switch (_type)
            {
                case WeaponType.Shell:
                    p = MakeProjectile();
                    p.rigid.velocity = vel;
                    break;
                case WeaponType.Rocket:
                    break;
                case WeaponType.Mine:
                    break;
                case WeaponType.MachineGun:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Projectile MakeProjectile()
        {
            var go = Instantiate<GameObject>(_def.projectilePrefab, _projectileAnchor, true);
            if(gameObject.CompareTag("Player")) {
                go.tag = "ProjectilePlayer";
            }
            
            go.transform.position = _muzzle.transform.position;
            go.transform.rotation = _muzzle.transform.rotation;
            
            var p = go.GetComponent<Projectile>();
            
            p.Type = Type;
            lastShotTime = Time.time;
            return(p);
        }
        
    }
}

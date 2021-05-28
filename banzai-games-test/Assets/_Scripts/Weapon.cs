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
        MachineGun, // Пулемет
        Rocket // Более мощный снаряд танка
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
        private PlayerShotController _shotController; 
        
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
            _shotController = gameObject.GetComponent<PlayerShotController>();
            if (_shotController == null) return;
            SetType(_shotController.type);
            _shotController.FireDelegate += Fire;
        }

        private static WeaponType Type { get; set; }

        public void SetType(WeaponType wt) {
            Type = wt;
            _def = Main.GetWeaponDefinition(Type);
            _muzzle = _shotController.muzzles[(int)wt]; // Получить позицию дула для последующих выстрелов 
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
            
            switch (Type)
            {
                case WeaponType.Shell:
                    p = MakeProjectile();
                    p.rigid.velocity = vel;
                    break;
                case WeaponType.Rocket:
                    p = MakeProjectile();
                    p.rigid.velocity = vel;
                    break;
                case WeaponType.MachineGun:
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
            if(gameObject.CompareTag("Player")) {
                go.tag = "ProjectilePlayer";
            }

            go.GetComponent<Light>().color = _def.colorLight;
            go.transform.position = _muzzle.transform.position;
            go.transform.rotation = _muzzle.transform.rotation;
            
            var p = go.GetComponent<Projectile>();
            
            p.Type = Type;
            _lastShotTime = Time.time;
            return(p);
        }
        
    }
}

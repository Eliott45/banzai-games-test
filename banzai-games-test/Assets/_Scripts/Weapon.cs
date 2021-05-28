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
        public WeaponType type = WeaponType.Shell;
        public GameObject projectilePrefab;
        public float damageOnHit = 50f;
        public float delayBetweenShots = 0f;
        public float velocity = 20f; // Скорость снаряда
    }

    public class Weapon : MonoBehaviour
    {
        private static WeaponType _type;
        
        private static Transform _projectileAnchor; // Пустой объект для корректного отображения снарядов иерархии 
        private PlayerShotController _shotController;
        
        public WeaponDefinition def;
        public GameObject muzzle;
        
        private void Start()
        {
            // Динамически создать точку привязки для всех снарядов
            if(_projectileAnchor == null) {
                var go = new GameObject("_ProjectileAnchor");
                _projectileAnchor = go.transform;
            }  
            
            _shotController = gameObject.GetComponent<PlayerShotController>();
            if (_shotController == null) return;
            SetType(_shotController.type);
            _shotController.FireDelegate += Fire;
        }

        private WeaponType Type {
            get => (_type);
            set => SetType(value);
        }

        private void SetType(WeaponType wt) {
            _type = wt;
            def = Main.GetWeaponDefinition(_type);
            muzzle = _shotController.muzzle;
        }
        
        /// <summary>
        /// Осуществляет выстрел.
        /// </summary>
        private void Fire()
        {
            Projectile p;
            
            var vel = transform.forward * def.velocity;
            
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
            var go = Instantiate<GameObject>(def.projectilePrefab, _projectileAnchor, true);
            if(gameObject.CompareTag("Player")) {
                go.tag = "ProjectilePlayer";
            }
            
            go.transform.position = muzzle.transform.position;
            go.transform.rotation = muzzle.transform.rotation;
            
            var p = go.GetComponent<Projectile>();
            
            p.Type = Type;
            return(p);
        }
        
    }
}

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
        Mine
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
        private static Transform _projectileAnchor; // Пустой объект для для корректного отображения иерархии 

        private void Start()
        {
            // Динамически создать точку привязки для всех снарядов
            if(_projectileAnchor == null) {
                var go = new GameObject("_ProjectileAnchor");
                _projectileAnchor = go.transform;
            }  
            
            var rootGO = transform.root.gameObject;
            if(rootGO.GetComponent<PlayerShotController>() != null) {
                rootGO.GetComponent<PlayerShotController>().fireDelegate += Fire;
            }
        }
        
        /// <summary>
        /// Осуществляет выстрел.
        /// </summary>
        private static void Fire()
        {
            Debug.Log("Shoot");
        }
    }
}

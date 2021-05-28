using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts
{
    public class PlayerShotController : MonoBehaviour
    {
        [Header("Set in Inspector")]
        public WeaponType type = WeaponType.Shell;
        public GameObject[] muzzles;
        
        private Weapon _weapon;
        
        public delegate void WeaponFireDelegate(); 
        public WeaponFireDelegate FireDelegate;

        private void Start()
        {
            _weapon = GetComponent<Weapon>();
        }

        private void Update()
        {
            if (Math.Abs(Input.GetAxis("Fire1") - 1) < 1)
            {
                FireDelegate?.Invoke();
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                SwitchWeaponUp();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchWeaponDown();
            }
        }
        
        /// <summary>
        /// Выбрать следующие оружие.
        /// </summary>
        private void SwitchWeaponUp()
        {
            if ((int)++type >= Enum.GetNames(typeof(WeaponType)).Length) type = 0;
            _weapon.SetType(type);
        }

        /// <summary>
        /// Выбрать предыдущие оружие.
        /// </summary>
        private void SwitchWeaponDown()
        {
            if ((int)--type < 0) type = (WeaponType) Enum.GetNames(typeof(WeaponType)).Length - 1;
            _weapon.SetType(type);
        }
        
    }
}

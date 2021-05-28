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
        [SerializeField] private GameObject[] _towers;
        
        private Weapon _weapon;
        
        public delegate void WeaponFireDelegate(); 
        public WeaponFireDelegate FireDelegate;

        private void Start()
        {
            _weapon = GetComponent<Weapon>();
            _towers[(int)type].SetActive(true);
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
            _towers[(int)type].SetActive(false);
            if ((int)++type >= Enum.GetNames(typeof(WeaponType)).Length) type = 0;
            SetWeapon();
        }

        /// <summary>
        /// Выбрать предыдущие оружие.
        /// </summary>
        private void SwitchWeaponDown()
        {
            _towers[(int)type].SetActive(false);
            if ((int)--type < 0) type = (WeaponType) Enum.GetNames(typeof(WeaponType)).Length - 1;
            SetWeapon();
        }
        
        /// <summary>
        /// Устанавливает новое оружие.
        /// </summary>
        private void SetWeapon()
        {
            _towers[(int)type].SetActive(true);
            _weapon.SetType(type);
        }
    }
}

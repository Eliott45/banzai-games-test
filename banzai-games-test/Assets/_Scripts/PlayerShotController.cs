using System;
using UnityEngine;

namespace _Scripts
{
    public class PlayerShotController : MonoBehaviour
    {
        [Header("Set in Inspector")]
        public WeaponType type = WeaponType.Shell;
        public GameObject[] muzzles;
        
        public delegate void WeaponFireDelegate(); 
        public WeaponFireDelegate FireDelegate;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                FireDelegate?.Invoke();
            }
        }
    }
}

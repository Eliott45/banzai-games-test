using System;
using UnityEngine;

namespace _Scripts
{
    public class PlayerShotController : MonoBehaviour
    {
        public delegate void WeaponFireDelegate(); 
        public WeaponFireDelegate fireDelegate;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                fireDelegate?.Invoke();
            }
        }
    }
}

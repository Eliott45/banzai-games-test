using System;
using UnityEngine;

namespace _Scripts
{
    public class Projectile : MonoBehaviour
    {
        private WeaponType _type;
        
        [Header("Set Dynamically")]
        public Rigidbody rigid;
        
        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        public WeaponType Type {
            get => (_type);
            set => SetType(value);
        }

        private void SetType(WeaponType eType) {
            _type = eType;
            var def = Main.GetWeaponDefinition(_type);
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }
    }
}

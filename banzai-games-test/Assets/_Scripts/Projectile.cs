using UnityEngine;

namespace _Scripts
{
    public class Projectile : MonoBehaviour
    {
        private WeaponType _type;
        
        [Header("Set Dynamically")]
        public Rigidbody rigid;
        /// <summary>
        /// Уменьшить или увеличить стандартный урон от снаряда.  
        /// </summary>
        public float multiplication;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        public WeaponType Type {
            get => (_type);
            set => SetType(value);
        }

        private void SetType(WeaponType eType)
        {
            _type = eType;
            Main.GetWeaponDefinition(_type);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Projectile")) return;
            Destroy(gameObject);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace _Scripts
{
    public class TrainingTarget : MonoBehaviour
    {
        [SerializeField] private float health = 1000f;
        [Range(0, 1f)] [SerializeField] private float armor = 0.2f;
        
        private void OnCollisionEnter(Collision other)
        {
            var otherGO = other.gameObject;
            switch (otherGO.tag)
            {
                case "ProjectilePlayer":
                    var p = otherGO.GetComponent<Projectile>();
                    TakeDamage(Main.GetWeaponDefinition(p.Type).damageOnHit);
                    break;
            }
        }

        private void TakeDamage(float damage)
        {
            health -= damage * (1f - armor);
            Debug.Log(health);
        }
    }
}

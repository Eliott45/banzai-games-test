using System;
using UnityEngine;
using System.Collections.Generic;

namespace _Scripts
{
    public class Main : MonoBehaviour
    {
        static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;

        [Header("Set weapon options:")]
        // Настройка свойст всех доступных типов оружий
        [SerializeField] private WeaponDefinition[] _weaponDefinitions;
        
        private void Awake()
        {
            WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
            foreach (var def in _weaponDefinitions)
            {
                WEAP_DICT[def.type] = def;
            }
        }
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Scripts
{
    public class Demolition : Enemy
    {
        [Header("Set options: Demolition")]
        [SerializeField] private float _timeDelay = 1f;
        
        private Light _light;
        private bool _flicker;

        protected override void Awake()
        {
            _light = GetComponent<Light>();
            base.Awake();
        }

        protected override void Update()
        {
            if (!_flicker)
            {
                StartCoroutine(FlickerLight());
            }
            
            base.Update();
        }

        private IEnumerator FlickerLight()
        {
            _flicker = true;
            _light.enabled = !_light.enabled;
            yield return new WaitForSeconds(_timeDelay);
            _flicker = false;
        }
    }
}

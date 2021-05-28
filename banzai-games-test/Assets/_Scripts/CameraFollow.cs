using UnityEngine;

namespace _Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Set in Inspector")]
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 1f;
        [SerializeField] private Vector3 _offset = new Vector3(2f,12f);
        
        private void FixedUpdate ()
        {
            var desiredPosition = _target.position + _offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(_target);
        }
    }
}

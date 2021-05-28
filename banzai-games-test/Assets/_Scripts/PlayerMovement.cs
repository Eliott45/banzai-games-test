using UnityEngine;

namespace _Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Set options of movement:")]
        [SerializeField] private float _speed = 5f;             
        [SerializeField] private float _turnSpeed = 180f;

        private Rigidbody _rigid;      
        
        private float _xAxis;         
        private float _yAxis;            

        private void Awake ()
        {
            _rigid = GetComponent<Rigidbody>();
        }
        
        private void Update ()
        {
            _xAxis = Input.GetAxis("Vertical");
            _yAxis = Input.GetAxis("Horizontal");
        }
        
        private void FixedUpdate ()
        {
            Move();
            Turn();
        }
        
        /// <summary>
        /// Движение танка, вперед и назад.
        /// </summary>
        private void Move ()
        {
            var movement = transform.forward * (_xAxis * _speed * Time.deltaTime); 
            _rigid.MovePosition(_rigid.position + movement);
        }

        /// <summary>
        /// Поворот танка.
        /// </summary>
        private void Turn ()
        {
            var turn = _yAxis * _turnSpeed * Time.deltaTime;
            var turnRotation = Quaternion.Euler(0f, turn, 0f);
            
            _rigid.MoveRotation (_rigid.rotation * turnRotation);
        }
    }
}

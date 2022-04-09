using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Camera _camera;
        private PlayerInputs _input;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _input = new PlayerInputs();
            _camera = Camera.allCameras[0];
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector2 input = _input.Player.Movement.ReadValue<Vector2>();

            if (input.magnitude > 0)
            {
                Move(input);
                Rotate(input);
            }
        }

        private void Move(Vector2 input)
        {
            input.Normalize();
            Vector3 direction = new Vector3(input.x, 0, input.y);
            direction = _camera.transform.rotation * direction;
            direction.y = 0;
            _rigidbody.MovePosition(_rigidbody.position + direction * Time.deltaTime * _speed);
        }

        private void Rotate(Vector2 input)
        {
            Vector3 direction = _camera.transform.rotation * new Vector3(input.x, 0, input.y);
            direction.y = 0;
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            Quaternion angle = Quaternion.RotateTowards(transform.rotation, targetAngle, 15f);
            transform.rotation = angle;
        }

        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();
    }
}
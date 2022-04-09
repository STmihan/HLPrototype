using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        private static readonly int Velocity = Animator.StringToHash("Velocity");
        
        [SerializeField] private float _speed;
        [SerializeField] private Animator _animator;

        private Camera _camera;
        private PlayerInputs _input;
        private Rigidbody _rigidbody;

        private bool _isAiming;
        
        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();
        
        private void Awake()
        {
            _input = new PlayerInputs();
            _camera = Camera.allCameras[0];
            _rigidbody = GetComponent<Rigidbody>();
            _input.Player.Fire1.performed += _ => _isAiming = true;
            _input.Player.Fire1.canceled += _ => _isAiming = false;
        }

        private void FixedUpdate()
        {
            Vector2 input = _input.Player.Movement.ReadValue<Vector2>();

            if (_isAiming)
            {
                input = Vector2.zero;
                AimState();
            }
            else MoveState(input);
            
            AnimationMove(input.normalized.magnitude);
        }

        private void AimState()
        {
            Vector2 mousePos = _input.Player.MousePosition.ReadValue<Vector2>();
            Ray ray = _camera.ScreenPointToRay(mousePos);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if(plane.Raycast(ray, out float d))
            {
                Vector3 point = ray.GetPoint(d);
                Vector3 direction = point - transform.position;
                RotateInDirection(direction.normalized, 95);
            }   
        }

        private void MoveState(Vector2 input)
        {
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
            RotateInDirection(direction, 15);
        }

        private void RotateInDirection(Vector3 direction, float maxDegreesDelta)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction, transform.up);
            Quaternion angle = Quaternion.RotateTowards(transform.rotation, targetAngle, maxDegreesDelta);
            transform.rotation = angle;
        }

        private void AnimationMove(float input)
        {
            float velocity = _animator.GetFloat(Velocity);

            if (input != 0) velocity += Time.deltaTime * 5;
            else velocity -= Time.deltaTime * 5;
            
            velocity = Mathf.Clamp(velocity, 0, 1);
            _animator.SetFloat(Velocity, velocity);
        }
    }
}
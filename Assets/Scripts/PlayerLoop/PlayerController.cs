using PlayerLoop.StateMachine;
using PlayerLoop.StateMachine.States;
using UnityEngine;

namespace PlayerLoop
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStats _stats;
        [Space]
        [SerializeField] private Animator _animator;

        private Camera _camera;
        private PlayerInputs _input;
        private Rigidbody _rigidbody;
        private PlayerStateMachine _stateMachine;

        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();
        
        private void Awake()
        {
            _input = new PlayerInputs();
            _camera = Camera.allCameras[0];
            _rigidbody = GetComponent<Rigidbody>();

            _stateMachine = new PlayerStateMachine();
            var data = new PlayerStateData(_camera, _input, _rigidbody, _stateMachine, _stats, _animator);
            _stateMachine.Initialize(new MovementPlayerState(data));
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace PlayerLoop.StateMachine.States
{
    public class MovementPlayerState : PlayerState
    {
        private static readonly int Velocity = Animator.StringToHash("Velocity");

        public MovementPlayerState(PlayerData data) : base(data) { }

        public override void Enter()
        {
            Data.Input.Player.Fire1.performed += EnterInAimState;
        }

        public override void Exit()
        {
            Data.Animator.SetFloat(Velocity, 0);
            Data.Input.Player.Fire1.performed -= EnterInAimState;
        }

        public override void FixedUpdate()
        {
            Vector2 input = Data.Input.Player.Movement.ReadValue<Vector2>();

            if (input.magnitude > 0)
            {
                Move(input);
                Rotate(input);
            }
            
            AnimationMove(input.normalized.magnitude);
        }
        
        private void Move(Vector2 input)
        {
            input.Normalize();
            Vector3 direction = new Vector3(input.x, 0, input.y);
            direction = Data.Camera.transform.rotation * direction;
            direction.y = 0;
            Data.Rigidbody.MovePosition(Data.Rigidbody.position + direction * Time.deltaTime * Data.Stats.Speed);
        }

        private void Rotate(Vector2 input)
        {
            Vector3 direction = Data.Camera.transform.rotation * new Vector3(input.x, 0, input.y);
            direction.y = 0;
            Data.Transform.RotateTransformInDirection(direction, 50);
        }
        
        private void AnimationMove(float input)
        {
            float velocity = Data.Animator.GetFloat(Velocity);

            if (input != 0) velocity += Time.deltaTime * 5;
            else velocity -= Time.deltaTime * 5;
            
            velocity = Mathf.Clamp(velocity, 0, 1);
            Data.Animator.SetFloat(Velocity, velocity);
        }


        private void EnterInAimState(InputAction.CallbackContext callbackContext)
        {
            Data.StateMachine.ChangeState(new WeaponPlayerState(Data));
        }
    }
}
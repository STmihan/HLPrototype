using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerLoop.StateMachine.States
{
    public class AimingPlayerState : PlayerState
    {
        public AimingPlayerState(PlayerStateData data) : base(data) { }

        public override void Enter()
        {
            Data.Input.Player.Fire1.canceled += EnterInMoveState;
        }
        
        public override void Exit()
        {
            Data.Input.Player.Fire1.canceled -= EnterInMoveState;
        }

        public override void FixedUpdate()
        {
            Vector2 mousePos = Data.Input.Player.MousePosition.ReadValue<Vector2>();
            Ray ray = Data.Camera.ScreenPointToRay(mousePos);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if(plane.Raycast(ray, out float d))
            {
                Vector3 point = ray.GetPoint(d);
                Vector3 direction = point - Data.Transform.position;
                PlayerUtils.RotateInDirection(Data.Transform, direction.normalized, 95);
            }  
        }
        
        private void EnterInMoveState(InputAction.CallbackContext obj)
        {
            Data.StateMachine.ChangeState(new MovementPlayerState(Data));
        }
    }
}
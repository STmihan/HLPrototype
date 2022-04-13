using UnityEngine.InputSystem;
using Weapons;

namespace PlayerLoop.StateMachine.States
{
    public class WeaponPlayerState : PlayerState
    {
        private Weapon Weapon => Data.ActiveWeapon;

        public WeaponPlayerState(PlayerData data, InputAction.CallbackContext callbackContext) : base(data)
        {
            Weapon.IsGamepad = callbackContext.control.device is Gamepad;
        }

        public override void Enter()
        {
            Weapon.WeaponAdd();
        }
        
        public override void Exit()
        {
            Weapon.WeaponRemove();
        }

        public override void FixedUpdate()
        {
            Weapon.WeaponUpdate();
        }
    }
}
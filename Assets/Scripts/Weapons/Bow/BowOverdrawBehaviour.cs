using PlayerLoop;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons.Bow
{
    public class BowOverdrawBehaviour : StateMachineBehaviour
    {
        private BowWeapon _weapon;
        private PlayerInputs _input;
        private Animator _animator;
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animator = animator;
            _weapon = PlayerController.Data.ActiveWeapon as BowWeapon;
            _input = PlayerController.Data.Input;
            _input.Player.Fire1.canceled += Fire1OnCanceled;
            if (_weapon != null) _weapon.Overdraw();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(_weapon.IsOverdrawing) _weapon.SetArrow();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _input.Player.Fire1.canceled -= Fire1OnCanceled;
        }

        private void Fire1OnCanceled(InputAction.CallbackContext obj)
        {
            _animator.SetBool(IsAiming, false);
            _weapon.Shoot();
        }
    }
}
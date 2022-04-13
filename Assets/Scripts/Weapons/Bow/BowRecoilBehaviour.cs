using PlayerLoop;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons.Bow
{
    public class BowRecoilBehaviour : StateMachineBehaviour
    {
        private Animator _animator;
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerController.Data.Input.Player.Fire1.performed += Fire1OnPerformed;
            _animator = animator;
        }

        private void Fire1OnPerformed(InputAction.CallbackContext obj)
        {
            _animator.SetBool(IsAiming, true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            BowWeapon weapon = PlayerController.Data.ActiveWeapon as BowWeapon;
            if (weapon == null) return;
            weapon.OnAttackEnd?.Invoke();
        }
    }
}
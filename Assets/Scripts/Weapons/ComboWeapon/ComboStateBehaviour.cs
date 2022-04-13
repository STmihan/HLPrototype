using PlayerLoop;
using UnityEngine;

namespace Weapons.ComboWeapon
{
    public class ComboStateBehaviour : StateMachineBehaviour
    {
        private ComboWeapon _weapon;
        private static readonly int Attack = Animator.StringToHash("Attack");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _weapon = PlayerController.Data.ActiveWeapon as ComboWeapon;
            if (_weapon != null) 
                _weapon.StartListenInput();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(_weapon == null) return;
            if (_weapon.InputReceived)
            {
                animator.SetTrigger(Attack);
                _weapon.StopListenInput();
            }
        }
    }
}
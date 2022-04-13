using PlayerLoop;
using UnityEngine;

namespace Weapons.CombatWeapon
{
    public class ComboStateBehaviour : StateMachineBehaviour
    {
        [SerializeField] private int _combo;
        private ComboWeapon _weapon;
        private static readonly int Combo = Animator.StringToHash("Combo");
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
                animator.SetInteger(Combo, _combo + 1);
                animator.SetTrigger(Attack);
                _weapon.StopListenInput();
            }
        }
    }
}
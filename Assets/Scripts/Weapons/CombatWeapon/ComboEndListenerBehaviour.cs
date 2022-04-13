using PlayerLoop;
using UnityEngine;

namespace Weapons.CombatWeapon
{
    public class ComboEndListenerBehaviour : StateMachineBehaviour
    {
        private bool _isFirstLoad = true;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_isFirstLoad)
            {
                _isFirstLoad = false;
                return;
            }
            ComboWeapon weapon = PlayerController.Data.ActiveWeapon as ComboWeapon;
            if(weapon == null) return;
            weapon.OnComboEnd?.Invoke();
        }
    }
}
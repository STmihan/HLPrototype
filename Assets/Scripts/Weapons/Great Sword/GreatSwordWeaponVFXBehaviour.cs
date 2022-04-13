using PlayerLoop;
using UnityEngine;

namespace Weapons.Great_Sword
{
    public class GreatSwordWeaponVFXBehaviour : StateMachineBehaviour
    {
        [SerializeField] private float _time;

        private GreatSwordWeapon _weapon;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _weapon = PlayerController.Data.ActiveWeapon as GreatSwordWeapon;

        }
    }
}
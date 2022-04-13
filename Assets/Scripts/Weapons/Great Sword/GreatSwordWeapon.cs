using System.Collections;
using System.Collections.Generic;
using PlayerLoop.StateMachine.States;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.CombatWeapon;

namespace Weapons.Great_Sword
{
    [CreateAssetMenu]
    public class GreatSwordWeapon : ComboWeapon
    {
        [SerializeField] private GreatSwordWeaponPrefab _weaponPrefab;

        protected override WeaponPrefab WeaponPrefab
        {
            get => _weaponPrefab;
            set => _weaponPrefab = value as GreatSwordWeaponPrefab;
        }

        public override void UpdateTransform(Transform leftHand, Transform rightHand)
        {
            WeaponPrefab.LeftHandPosition.position = leftHand.position;
            WeaponPrefab.LeftHandPosition.rotation = leftHand.rotation;
            
            WeaponPrefab.RightHandPosition.position = rightHand.position;
            WeaponPrefab.RightHandPosition.rotation = rightHand.rotation;
        }

        public override void WeaponAdd()
        {
            base.WeaponAdd();
            Data.Animator.SetLayerWeight(1, 0);
            Data.Input.Player.Fire1.performed += Fire1OnPerformed;
            OnComboEnd += () => Data.StateMachine.ChangeState(new MovementPlayerState(Data));
            Data.OnAnimationString += OnAnimationString;
        }

        private void Fire1OnPerformed(InputAction.CallbackContext obj) => Attack();

        private void OnAnimationString(string str)
        {
            if(str == "PlayVFX")
                _weaponPrefab.PlayVFX();
        }

        public override void WeaponRemove()
        {
            base.WeaponRemove();
            Data.Animator.SetLayerWeight(1, 1);
            Data.OnAnimationString -= OnAnimationString;
            Data.Input.Player.Fire1.performed -= Fire1OnPerformed;
        }
    }
}
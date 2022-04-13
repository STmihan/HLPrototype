using PlayerLoop.StateMachine.States;
using UnityEngine;
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
            Attack();
            Data.Input.Player.Fire1.performed += _ => Attack();
            OnComboEnd += () => Data.StateMachine.ChangeState(new MovementPlayerState(Data));
        }

        public override void WeaponRemove()
        {
            base.WeaponRemove();
            Data.Animator.SetLayerWeight(1, 1);
        }
    }
}
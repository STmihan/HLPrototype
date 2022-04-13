using System;
using PlayerLoop.StateMachine.States;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Weapons.Bow
{
    [CreateAssetMenu]
    public class BowWeapon : Weapon
    {
        [SerializeField] private float _arrowForce;
        
        [SerializeField] private BowWeaponPrefab _weaponPrefab;
        [SerializeField] private BowWeaponArrowPrefab _arrowPrefab;

        public bool IsOverdrawing { get; private set; }
        public Action OnAttackEnd;
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");

        private BowWeaponArrowPrefab _arrow;

        protected override WeaponPrefab WeaponPrefab
        {
            get => _weaponPrefab;
            set => _weaponPrefab = value as BowWeaponPrefab;
        }

        public override void UpdateTransform(Transform leftHand, Transform rightHand)
        {
            WeaponPrefab.LeftHandPosition.position = leftHand.position;
            WeaponPrefab.LeftHandPosition.rotation = leftHand.rotation;
            WeaponPrefab.LeftHandPosition.Rotate(0, 0, 90f, Space.Self);
            if (IsOverdrawing)
            {
                WeaponPrefab.RightHandPosition.position = Vector3.Lerp(WeaponPrefab.RightHandPosition.position,
                    rightHand.position, Time.deltaTime * 30);
                WeaponPrefab.RightHandPosition.rotation = rightHand.rotation;
            }
            else
            {
                WeaponPrefab.RightHandPosition.position = Vector3.Lerp(WeaponPrefab.RightHandPosition.position,
                    _weaponPrefab.StringPosition.position, Time.deltaTime * 30);
                WeaponPrefab.RightHandPosition.rotation = rightHand.rotation;
            }
        }

        public override void WeaponAdd()
        {
            base.WeaponAdd();
            OnAttackEnd += EndAttack;
            Data.Animator.SetBool(IsAiming, true);
            Data.Input.Player.Fire1.canceled += OnCancel;
        }

        private void EndAttack()
        {
            Data.StateMachine.ChangeState(new MovementPlayerState(Data));
        }

        public void Overdraw()
        {
            IsOverdrawing = true;
            Data.Input.Player.Fire1.canceled -= OnCancel;
            _arrow = Instantiate(_arrowPrefab);
        }

        public void SetArrow()
        {
            _arrow.transform.position = _weaponPrefab.RightHandPosition.position;
            _arrow.transform.LookAt(_weaponPrefab.LeftHandPosition.position);
            // _arrow.transform.Rotate(_arrow.transform.right, 60f, Space.Self);
        }

        public void Shoot()
        {
            IsOverdrawing = false;
            _arrow.Shoot(_arrowForce);
            _arrow = null;
        }

        public void Cancel()
        {
            Data.Animator.SetBool(IsAiming, false);
            Data.Animator.SetTrigger("Cancel");
            EndAttack();
        }

        private void OnCancel(InputAction.CallbackContext obj)
        {
            if(!IsOverdrawing) Cancel();
        }
    }
}

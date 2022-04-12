using System;
using PlayerLoop.StateMachine;
using PlayerLoop.StateMachine.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons.Bow
{
    [CreateAssetMenu]
    public class BowWeapon : Weapon
    {
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");

        [SerializeField] private string _overdrawClipName = "Standing Aim Overdraw";
        [SerializeField] private BowWeaponPrefab _weaponPrefab;

        private PlayerData _data;
        
        private bool _isOverdrawing;
        private event Action<bool> ChangeOverdrawing;

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
            if (_isOverdrawing)
            {
                WeaponPrefab.RightHandPosition.position = Vector3.Lerp(WeaponPrefab.RightHandPosition.position,
                    rightHand.position, Time.deltaTime * 15);
                WeaponPrefab.RightHandPosition.rotation = rightHand.rotation;
            }
            else
            {
                WeaponPrefab.RightHandPosition.position = Vector3.Lerp(WeaponPrefab.RightHandPosition.position,
                    _weaponPrefab.StringPosition.position, Time.deltaTime * 15);
                WeaponPrefab.RightHandPosition.rotation = rightHand.rotation;
            }
        }

        public override void WeaponAdd(PlayerData data)
        {
            _data = data;
            data.Animator.SetBool(IsAiming, true);
            data.Input.Player.Fire1.canceled += EnterInMoveState;
            _isOverdrawing = false;
            ChangeOverdrawing += delegate(bool b) { _isOverdrawing = b; };
        }

        public override void WeaponUpdate(PlayerData data)
        {
            base.WeaponUpdate(data);
            Overdraw();
        }

        public override void WeaponRemove(PlayerData data)
        {
            data.Animator.SetBool(IsAiming, false);
            _isOverdrawing = false;
            data.Input.Player.Fire1.canceled -= EnterInMoveState;
        }

        private void EnterInMoveState(InputAction.CallbackContext callbackContext)
        {
            _data.StateMachine.ChangeState(new MovementPlayerState(_data));
        }
        
        private void Overdraw()
        {
            bool isName = _data.Animator.GetCurrentAnimatorStateInfo(1).IsName(_overdrawClipName);
            if (_isOverdrawing != isName) ChangeOverdrawing?.Invoke(isName);
        }
    }
}
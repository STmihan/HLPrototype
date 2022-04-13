using System;
using PlayerLoop.StateMachine;
using PlayerLoop.StateMachine.States;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace PlayerLoop
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public static PlayerData Data { get; private set; }
        
        [SerializeField] private PlayerStats _stats;
        [Space]
        [SerializeField] private Animator _animator;

        [Space, Header("Weapon")] 
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] private Transform _weaponParent;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;

        private Camera _camera;
        private Rigidbody _rigidbody;
        private PlayerStateMachine _stateMachine;
        private Weapon _activeWeapon;
        private PlayerInputs _input;

        private WeaponSwitchInput _weaponSwitchInput;
        
        private void OnEnable()
        {
            _input.Enable();
            _weaponSwitchInput.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
            _weaponSwitchInput.Disable();
        }

        private void Awake()
        {
            _input = new PlayerInputs();
            _weaponSwitchInput = new WeaponSwitchInput();
            _camera = Camera.allCameras[0];
            _rigidbody = GetComponent<Rigidbody>();
            _stateMachine = new PlayerStateMachine();
            WeaponSwitcherRegister();
            
            Data = new PlayerData(_camera, _input, _rigidbody, _stateMachine, _stats, _animator);
            EquipWeapon(_weapons[0]);
            _stateMachine.Initialize(new MovementPlayerState(Data));
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        private void Update()
        {
            _stateMachine.Update();
            _activeWeapon.UpdateTransform(_leftHand, _rightHand);
        }
        public void EquipWeapon(Weapon weapon)
        {
            if(weapon == null) return;
            if (_activeWeapon != null)
            {
                _activeWeapon.DestroyPrefab();
                Destroy(_activeWeapon);
            }
            _activeWeapon = Instantiate(weapon);
            _animator.runtimeAnimatorController = _activeWeapon.Controller;
            _activeWeapon.SpawnPrefab(_weaponParent);
            _activeWeapon.UpdateTransform(_leftHand, _rightHand);
            Data.SetWeapon(_activeWeapon);
        }

        private void WeaponSwitcherRegister()
        {
            _weaponSwitchInput.Weapon._1.performed += delegate { EquipWeapon(_weapons[0]); };
            _weaponSwitchInput.Weapon._2.performed += delegate { EquipWeapon(_weapons[1]); };
            _weaponSwitchInput.Weapon._3.performed += delegate { EquipWeapon(_weapons[2]); };
        }
    }
}
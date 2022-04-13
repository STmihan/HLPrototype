using System;
using UnityEngine;
using Weapons;

namespace PlayerLoop.StateMachine
{
    public class PlayerData
    {
        public PlayerData(Camera camera, PlayerInputs input, CharacterController characterController, PlayerStateMachine stateMachine, PlayerStats stats, Animator animator)
        {
            Camera = camera;
            Input = input;
            CharacterController = characterController;
            StateMachine = stateMachine;
            Stats = stats;
            Animator = animator;
        }

        public Camera Camera { get; }
        public PlayerInputs Input { get; }
        public CharacterController CharacterController { get; }
        public PlayerStateMachine StateMachine { get; }
        public PlayerStats Stats { get; }
        public Animator Animator { get; }
        public Transform Transform => CharacterController.transform;
        public Weapon ActiveWeapon { get; private set; }
        public Action<string> OnAnimationString { get; set; }

        public void SetWeapon(Weapon weapon) => ActiveWeapon = weapon;
    }
}
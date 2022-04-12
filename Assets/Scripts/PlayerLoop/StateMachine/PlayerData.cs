using UnityEngine;
using Weapons;

namespace PlayerLoop.StateMachine
{
    public class PlayerData
    {
        public PlayerData(Camera camera, PlayerInputs input, Rigidbody rigidbody, PlayerStateMachine stateMachine, PlayerStats stats, Animator animator)
        {
            Camera = camera;
            Input = input;
            Rigidbody = rigidbody;
            StateMachine = stateMachine;
            Stats = stats;
            Animator = animator;
        }

        public Camera Camera { get; }
        public PlayerInputs Input { get; }
        public Rigidbody Rigidbody { get; }
        public PlayerStateMachine StateMachine { get; }
        public PlayerStats Stats { get; }
        public Animator Animator { get; }
        public Transform Transform => Rigidbody.transform;
        public Weapon ActiveWeapon { get; private set; }

        public void SetWeapon(Weapon weapon) => ActiveWeapon = weapon;
    }
}
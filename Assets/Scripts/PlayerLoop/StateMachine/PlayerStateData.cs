using UnityEngine;

namespace PlayerLoop.StateMachine
{
    public class PlayerStateData
    {
        public PlayerStateData(Camera camera, PlayerInputs input, Rigidbody rigidbody, PlayerStateMachine stateMachine, PlayerStats stats, Animator animator, Weapon activeWeapon)
        {
            Camera = camera;
            Input = input;
            Rigidbody = rigidbody;
            StateMachine = stateMachine;
            Stats = stats;
            Animator = animator;
            ActiveWeapon = activeWeapon;
        }

        public Camera Camera { get; }
        public PlayerInputs Input { get; }
        public Rigidbody Rigidbody { get; }
        public PlayerStateMachine StateMachine { get; }
        public PlayerStats Stats { get; }
        public Animator Animator { get; }
        public Transform Transform => Rigidbody.transform;
        public Weapon ActiveWeapon { get; }
    }
}
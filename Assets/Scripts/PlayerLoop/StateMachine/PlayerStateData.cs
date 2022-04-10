using UnityEngine;

namespace PlayerLoop.StateMachine
{
    public class PlayerStateData
    {
        public PlayerStateData(Camera camera, PlayerInputs input, Rigidbody rigidbody, PlayerStateMachine stateMachine, PlayerStats stats, Animator animator)
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
    }
}
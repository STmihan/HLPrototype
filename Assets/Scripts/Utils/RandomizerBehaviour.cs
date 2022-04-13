using UnityEngine;

namespace Utils
{
    public class RandomizerBehaviour : StateMachineBehaviour
    {
        private static readonly int RandomI = Animator.StringToHash("RandomI");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetInteger(RandomI, Random.Range(0, 101));
        }
    }
}
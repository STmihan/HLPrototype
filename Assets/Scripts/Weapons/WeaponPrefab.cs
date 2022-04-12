using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Animator))]
    public abstract class WeaponPrefab : MonoBehaviour
    {
        [SerializeField] protected Transform _leftHandPosition;
        [SerializeField] protected Transform _rightHandPosition;

        public Transform LeftHandPosition => _leftHandPosition;
        public Transform RightHandPosition => _rightHandPosition;

        protected Animator WeaponAnimator { get; private set; }

        private void Awake()
        {
            WeaponAnimator = GetComponent<Animator>();
        }
    }
}
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponPrefab : MonoBehaviour
    {
        [SerializeField] protected Transform _leftHandPosition;
        [SerializeField] protected Transform _rightHandPosition;

        public Transform LeftHandPosition => _leftHandPosition;
        public Transform RightHandPosition => _rightHandPosition;
    }
}
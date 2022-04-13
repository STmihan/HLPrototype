using UnityEngine;

namespace Weapons.Bow
{
    [RequireComponent(typeof(Collider))]
    public class BowWeaponArrowPrefab : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private TrailRenderer _trail;
        private Collider _collider;

        private void Awake()
        {
            _rigidbody = GetComponentInChildren<Rigidbody>();
            _trail = GetComponentInChildren<TrailRenderer>();
            _collider = GetComponentInChildren<Collider>();
            _collider.enabled = false;
            _trail.enabled = false;
            _rigidbody.isKinematic = true;
        }

        public void Shoot(float force)
        {
            _rigidbody.isKinematic = false;
            _trail.enabled = true;
            _collider.enabled = true;
            _rigidbody.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
            Destroy(gameObject, 5f);
        }

        private void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.layer == LayerMask.NameToLayer("Player")) return;
            if(col.gameObject.layer == LayerMask.NameToLayer("Ground")) return;
            _rigidbody.isKinematic = true;
            Destroy(gameObject);
        }
    }
}
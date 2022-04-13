using PlayerLoop;
using PlayerLoop.StateMachine;
using UnityEngine;
using Utils;

namespace Weapons
{
    public abstract class Weapon : ScriptableObject
    {
        [SerializeField] protected RuntimeAnimatorController _controller;
        protected abstract WeaponPrefab WeaponPrefab { get; set; }
        public RuntimeAnimatorController Controller => _controller;

        protected PlayerData Data;

        public virtual void UpdateTransform(Transform leftHand, Transform rightHand) { }

        public virtual void SpawnPrefab(Transform parent)
        {
            WeaponPrefab = Instantiate(WeaponPrefab, parent);
        }

        public virtual void DestroyPrefab()
        {
            Destroy(WeaponPrefab.gameObject);
        }

        public virtual void WeaponAdd()
        {
            Data = PlayerController.Data;
        }

        public virtual void WeaponUpdate()
        {
            Vector2 mousePos = Data.Input.Player.MousePosition.ReadValue<Vector2>();
            Ray ray = Data.Camera.ScreenPointToRay(mousePos);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if(plane.Raycast(ray, out float d))
            {
                Vector3 point = ray.GetPoint(d);
                Vector3 direction = point - Data.Transform.position;
                Data.Transform.RotateTransformInDirection(direction.normalized, 95);
            }
        }

        public virtual void WeaponRemove() { }
    }
}
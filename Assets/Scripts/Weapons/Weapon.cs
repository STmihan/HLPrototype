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

        public virtual void UpdateTransform(Transform leftHand, Transform rightHand) { }

        public virtual void SpawnPrefab(Transform parent)
        {
            WeaponPrefab = Instantiate(WeaponPrefab, parent);
        }

        public virtual void DestroyPrefab()
        {
            Destroy(WeaponPrefab.gameObject);
        }

        public virtual void WeaponAdd(PlayerData data) { }

        public virtual void WeaponUpdate(PlayerData data)
        {
            Vector2 mousePos = data.Input.Player.MousePosition.ReadValue<Vector2>();
            Ray ray = data.Camera.ScreenPointToRay(mousePos);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if(plane.Raycast(ray, out float d))
            {
                Vector3 point = ray.GetPoint(d);
                Vector3 direction = point - data.Transform.position;
                data.Transform.RotateTransformInDirection(direction.normalized, 95);
            }
        }
        public virtual void WeaponRemove(PlayerData data) { }
    }
}
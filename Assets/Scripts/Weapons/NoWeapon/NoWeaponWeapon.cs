using PlayerLoop.StateMachine.States;
using UnityEngine;

namespace Weapons.NoWeapon
{
    [CreateAssetMenu]
    public class NoWeaponWeapon : Weapon
    {
        protected override WeaponPrefab WeaponPrefab { get; set; }

        public override void WeaponAdd()
        {
            base.WeaponAdd();
            Data.StateMachine.ChangeState(new MovementPlayerState(Data));
        }

        public override void DestroyPrefab() { }
        public override void SpawnPrefab(Transform parent) { }

        public override void WeaponUpdate() { }
    }
}

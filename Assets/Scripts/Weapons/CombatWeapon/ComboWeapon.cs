using System;

namespace Weapons.CombatWeapon
{
    public class ComboWeapon : Weapon
    {
        public bool CanReceiveInput { get; private set; }
        public bool InputReceived { get; private set; }
        
        public Action OnComboEnd;

        protected override WeaponPrefab WeaponPrefab { get; set; }

        protected void Attack()
        {
            if (!CanReceiveInput) return;
            InputReceived = true;
            CanReceiveInput = false;
        }

        public void StartListenInput()
        {
            CanReceiveInput = true;
        }
        
        public void StopListenInput()
        {
            CanReceiveInput = !CanReceiveInput;
            InputReceived = false;
        }
    }
}
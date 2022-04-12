using Weapons;

namespace PlayerLoop.StateMachine.States
{
    public class WeaponPlayerState : PlayerState
    {
        private Weapon Weapon => Data.ActiveWeapon;
        public WeaponPlayerState(PlayerData data) : base(data) { }

        public override void Enter()
        {
            Weapon.WeaponAdd(Data);
        }
        
        public override void Exit()
        {
            Weapon.WeaponRemove(Data);
        }

        public override void FixedUpdate()
        {
            Weapon.WeaponUpdate(Data);
        }
    }
}
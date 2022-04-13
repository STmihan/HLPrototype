namespace PlayerLoop.StateMachine.States
{
    public abstract class PlayerState
    {
        protected readonly PlayerData Data;

        protected PlayerState(PlayerData data)
        {
            Data = data;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void FixedUpdate()
        {
            
        }

        public virtual void Exit()
        {
            
        }
    }
}
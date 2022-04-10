namespace PlayerLoop.StateMachine.States
{
    public abstract class PlayerState
    {
        protected PlayerStateData Data;

        protected PlayerState(PlayerStateData data)
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
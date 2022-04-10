using PlayerLoop.StateMachine.States;

namespace PlayerLoop.StateMachine
{
    public class PlayerStateMachine
    {
        private PlayerState _activeState;

        public void Initialize(PlayerState state)
        {
            _activeState = state;
            _activeState.Enter();
        }

        public void Update()
        {
            _activeState.Update();
        }

        public void FixedUpdate()
        {
            _activeState.FixedUpdate();
        }

        public void ChangeState(PlayerState state)
        {
            _activeState.Exit();
            _activeState = state;
            state.Enter();
        }
    }
}
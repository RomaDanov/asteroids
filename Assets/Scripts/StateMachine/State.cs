namespace StateMachine
{
	public abstract class State
	{
		private StateMachineBehaviour stateMachine;

		internal void SetStateMachine(StateMachineBehaviour stateMachine)
		{
			this.stateMachine = stateMachine;
		}

		internal void Finish()
		{
			stateMachine.SwitchNextState();
		}

		internal void Return()
		{
			stateMachine.SwitchPrevState();
		}

		internal void Finish(string stateId)
		{
			stateMachine.SwitchState(stateId);
		}

		internal void Finish<T>() where T : State
		{
			stateMachine.SwitchState<T>();
		}

		internal virtual void Awake() { }
		internal virtual void Enter() { }
		internal virtual void Update() { }
		internal virtual void Exit() { }
		internal virtual void Dispose() { }
	}
}
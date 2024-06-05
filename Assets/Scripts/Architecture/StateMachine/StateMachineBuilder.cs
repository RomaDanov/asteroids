namespace Architecture.StateMachine
{
	public class StateMachineBuilder
	{
		private readonly StateMachineBehaviour stateMachine;

		public StateMachineBuilder(string stateMachineName)
		{
			stateMachine = new StateMachineBehaviour(stateMachineName);
		}

		public StateMachineBuilder State(State state)
		{
			stateMachine.AddState(state);
			return this;
		}

		public StateMachineBuilder State<TState>() where TState : State, new()
		{
			stateMachine.AddState(new TState());
			return this;
		}

		public StateMachineBehaviour Build()
		{
			return stateMachine;
		}
	}
}
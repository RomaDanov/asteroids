using System.Collections.Generic;
using System.Linq;

namespace StateMachine
{
	public class StateMachineBehaviour
	{
		private Dictionary<string, State> states = new();
		private State currentState;

		public string Name { get; private set; }
		public bool IsActive { get; private set; }
		public string CurrentStateId { get; private set; }

		public StateMachineBehaviour(string name)
		{
			Name = name;
		}

		public void Start<TState>() where TState : State
		{
			if (IsActive) return;

			IsActive = true;
			SwitchState<TState>();
		}

		public void Stop()
		{
			if (!IsActive) return;

			IsActive = false;
			currentState?.Exit();
			currentState = null;
		}

		public TState AddState<TState>(string id = "") where TState : State, new()
		{
			var state = new TState();
			return (TState)AddState(state, id);
		}

		public State AddState(State state, string id = "")
		{
			string stateId = string.IsNullOrEmpty(id) ? state.GetType().Name : id;
			states.Add(stateId, state);
			state.SetStateMachine(this);
			state.Awake();

			return state;
		}

		public void SwitchState(string stateId)
		{
			if (!IsActive) return;

			if (CurrentStateId == stateId) return;

			currentState?.Exit();
			currentState = null;

			if (states.TryGetValue(stateId, out State state))
			{
				currentState = state;
				CurrentStateId = stateId;
				currentState.Enter();
			}
		}

		public void SwitchState<TState>() where TState : State
		{
			var stateType = typeof(TState);
			SwitchState(stateType.Name);
		}

		public void SwitchNextState()
		{
			if (!IsActive) return;
			if (currentState == null) return;

			int currentStateIndex = states.ToList().FindIndex(x => x.Key == CurrentStateId);
			int nextStateIndex = currentStateIndex + 1;
			if (nextStateIndex >= states.Count)
			{
				currentState?.Exit();
				currentState = null;
				return;
			}

			string nextStateId = states.ElementAt(nextStateIndex).Key;
			SwitchState(nextStateId);
		}

		public void Update()
		{
			if (!IsActive) return;

			currentState?.Update();
		}

		public void Dispose()
		{
			foreach (var kvp in states)
			{
				kvp.Value.Dispose();
			}
			states.Clear();
		}
	}
}
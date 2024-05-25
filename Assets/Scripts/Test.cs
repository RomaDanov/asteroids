using Inputs;
using ObjectPool;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
	[SerializeField] private TestObject testObject;

	private StateMachine.StateMachineBehaviour fsm;

	private void Awake()
	{
		fsm = new StateMachineBuilder("TestMachine")
			.State(new InitState("Init"))
			.State<FirstState>()
			.State(new SecondState("Second"))
			.Build();
	}

	private void Start()
	{
		fsm.Start<InitState>();
	}

	private void Update()
	{
		IInput input = InputManager.Instance.Input;

		Debug.Log($"[InputManager] Accelerate: {input.GetAccelerate()}");
		Debug.Log($"[InputManager] Rotation: {input.GetRotation()}");
		Debug.Log($"[InputManager] Fire: {input.GetFire()}");
		Debug.Log($"[InputManager] Alternative fire: {input.GetAlternativeFire()}");

		if (Keyboard.current.aKey.wasPressedThisFrame)
		{
			var pool = ObjectPoolService.Instance.GetOrCreatePool(testObject, 2);
			TestObject obj = pool.Get(transform);
		}

		fsm?.Update();

		if (Keyboard.current.digit1Key.wasPressedThisFrame)
		{
			fsm.SwitchState<FirstState>();
		}
		else if (Keyboard.current.digit2Key.wasPressedThisFrame)
		{
			fsm.SwitchState<SecondState>();
		}

		if (Keyboard.current.sKey.wasPressedThisFrame)
		{
			if (fsm.IsActive)
			{
				fsm.Stop();
			}
			else
			{
				fsm.Start<InitState>();
			}
		}
	}

	private class InitState : State
	{
		private string message = "InitState";

		public InitState() { }

		public InitState(string message)
		{
			this.message = message;
		}

		internal override void Awake()
		{
			Debug.Log($"[StateMachine] Awake: {message}");
		}

		internal override void Enter()
		{
			Debug.Log($"[StateMachine] Enter: {message}");
			Finish();
		}

		internal override void Update()
		{
			Debug.Log($"[StateMachine] Update: {message}");
		}

		internal override void Exit()
		{
			Debug.Log($"[StateMachine] Exit: {message}");
		}

		internal override void Dispose()
		{
			Debug.Log($"[StateMachine] Dispose: {message}");
		}
	}

	private class FirstState : State
	{
		private string message = "FirstState";

		public FirstState() { }

		public FirstState(string message)
		{
			this.message = message;
		}

		internal override void Awake()
		{
			Debug.Log($"[StateMachine] Awake: {message}");
		}

		internal override void Enter()
		{
			Debug.Log($"[StateMachine] Enter: {message}");
		}

		internal override void Update()
		{
			Debug.Log($"[StateMachine] Update: {message}");
			if (Keyboard.current.fKey.wasPressedThisFrame)
			{
				Finish<SecondState>();
			}
		}

		internal override void Exit()
		{
			Debug.Log($"[StateMachine] Exit: {message}");
		}

		internal override void Dispose()
		{
			Debug.Log($"[StateMachine] Dispose: {message}");
		}
	}

	private class SecondState : State
	{
		private string message = "SecondState";

		public SecondState() { }

		public SecondState(string message)
		{
			this.message = message;
		}

		internal override void Awake()
		{
			Debug.Log($"[StateMachine] Awake: {message}");
		}

		internal override void Enter()
		{
			Debug.Log($"[StateMachine] Enter: {message}");
		}

		internal override void Update()
		{
			Debug.Log($"[StateMachine] Update: {message}");
			if (Keyboard.current.fKey.wasPressedThisFrame)
			{
				Finish();
			}
		}

		internal override void Exit()
		{
			Debug.Log($"[StateMachine] Exit: {message}");
		}

		internal override void Dispose()
		{
			Debug.Log($"[StateMachine] Dispose: {message}");
		}
	}
}

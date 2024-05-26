using Inputs;
using UnityEngine;

public class Test : MonoBehaviour
{
	private void Start()
	{
		Debug.Log($"{InputManager.Instance.CurrentType}");
	}

	private void OnDestroy()
	{
		InputManager.Instance.Dispose();
		Debug.Log("Dispose");
	}
}

using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] private float rotationSpeed;

	private void Update()
	{
		Rotate(rotationSpeed * Time.deltaTime);
	}

	private void Rotate(float speed)
	{
		transform.up = Quaternion.AngleAxis(speed, Vector3.forward) * transform.up;
	}
}

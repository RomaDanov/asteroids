using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
	[SerializeField] private Transform target;

	[SerializeField] private float returnSpeed;
	[SerializeField] private float rearDistance;

	private Vector3 currentVector;

	private void Start()
	{
		currentVector = new Vector3(target.position.x, target.position.y, target.position.z - rearDistance);
		transform.position = currentVector;
	}

	private void FixedUpdate()
	{
		if (target == null) return;

		currentVector = new Vector3(target.position.x, target.position.y, target.position.z - rearDistance);
		transform.position = Vector3.Lerp(transform.position, currentVector, returnSpeed * Time.fixedDeltaTime);
	}
}

using UnityEngine;
using UnityEngine.InputSystem;

public class AsteroidMovement : MonoBehaviour, IMovable
{
	private Vector3 moveDirecion;
	private float moveSpeed;
	private float torq;

	public void Configure(Vector2 moveSpeedRange, Vector2 rotateSpeedRange)
	{
		moveDirecion = (Vector3.zero - transform.position).normalized;
		moveSpeed = Random.Range(moveSpeedRange.x, moveSpeedRange.y);
		torq = Random.Range(rotateSpeedRange.x, rotateSpeedRange.y);
	}

	/// TEST
	private void Update()
	{
		if (Keyboard.current.qKey.wasPressedThisFrame)
		{
			Configure(new Vector2(0.2f, 2), new Vector2(-100, 100));
		}
	}
	/// TEST

	private void FixedUpdate()
	{
		Move();
		Rotate();
	}

	private void Move()
	{
		transform.position += moveDirecion * moveSpeed * Time.fixedDeltaTime;
	}

	private void Rotate()
	{
		transform.up = Quaternion.AngleAxis(torq * Time.fixedDeltaTime, Vector3.forward) * transform.up;
	}
}

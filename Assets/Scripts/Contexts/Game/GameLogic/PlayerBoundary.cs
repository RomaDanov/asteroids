using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
	[SerializeField] private Transform controllTransform;
	[SerializeField] private float offset;

	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void Update()
	{
		Vector3 min = mainCamera.ScreenToWorldPoint(new Vector3(0, 0));
		Vector3 max = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight));

		min.x += offset;
		min.y += offset;

		max.x -= offset;
		max.y -= offset;

		Vector3 clampedPosition = controllTransform.position;

		if (controllTransform.position.x < min.x)
		{
			clampedPosition.x = min.x;
		}
		else if (controllTransform.position.x > max.x)
		{
			clampedPosition.x = max.x;
		}

		if (controllTransform.position.y < min.y)
		{
			clampedPosition.y = min.y;
		}
		else if (controllTransform.position.y > max.y)
		{
			clampedPosition.y = max.y;
		}

		controllTransform.position = clampedPosition;
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (controllTransform == null) return;
		Gizmos.DrawWireSphere(controllTransform.position, offset);
	}
#endif
}

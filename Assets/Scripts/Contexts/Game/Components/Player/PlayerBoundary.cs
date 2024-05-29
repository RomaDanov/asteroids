using UnityEngine;

namespace Contexts.Game.Components.Player
{
	public class PlayerBoundary : MonoBehaviour
	{
		[SerializeField] private float size;

		private Camera mainCamera;

		private void Start()
		{
			mainCamera = Camera.main;
		}

		private void Update()
		{
			Vector3 min = mainCamera.ScreenToWorldPoint(new Vector3(0, 0));
			Vector3 max = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight));

			min.x += size;
			min.y += size;

			max.x -= size;
			max.y -= size;

			Vector3 clampedPosition = transform.position;

			if (transform.position.x < min.x)
			{
				clampedPosition.x = min.x;
			}
			else if (transform.position.x > max.x)
			{
				clampedPosition.x = max.x;
			}

			if (transform.position.y < min.y)
			{
				clampedPosition.y = min.y;
			}
			else if (transform.position.y > max.y)
			{
				clampedPosition.y = max.y;
			}

			transform.position = clampedPosition;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, size);
		}
#endif
	}
}
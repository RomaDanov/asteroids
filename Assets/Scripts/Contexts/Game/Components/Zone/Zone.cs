using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Contexts.Game.Components.Zone
{
	public abstract class Zone<T> : MonoBehaviour where T : IZoneVisitor
	{
		[SerializeField] protected Boundary boundary;
		[SerializeField] protected LayerMask targetLayers;
		[Space]
		[Header("Editor")]
		[SerializeField] private Color color;


		protected abstract void VisitObjects(List<T> visitors);

		private void FixedUpdate()
		{
			List<T> visitors = new();

			Vector2 topLeftPoint = (Vector2)transform.position + new Vector2(boundary.Min.x, boundary.Max.y);
			bool topReached = CastRay(topLeftPoint, Vector2.right, boundary.Width, ref visitors);

			Vector2 topRightPoint = (Vector2)transform.position + new Vector2(boundary.Max.x, boundary.Max.y);
			bool rightReached = CastRay(topRightPoint, Vector2.down, boundary.Height, ref visitors);

			Vector2 bottomRightPoint = (Vector2)transform.position + new Vector2(boundary.Max.x, boundary.Min.y);
			bool bottomReached = CastRay(bottomRightPoint, Vector2.left, boundary.Width, ref visitors);

			Vector2 bototmLeftPoint = (Vector2)transform.position + new Vector2(boundary.Min.x, boundary.Min.y);
			bool leftReached = CastRay(bototmLeftPoint, Vector2.up, boundary.Height, ref visitors);

			bool anyReached = topReached || rightReached || bottomReached || leftReached;

			if (anyReached)
			{
				VisitObjects(visitors);
			}
		}

		protected bool CastRay(Vector3 worldPosition, Vector3 direction, float distance, ref List<T> visitors)
		{
			bool success = false;
			RaycastHit2D[] hitInfo = Physics2D.RaycastAll(worldPosition, direction, distance, targetLayers);
			if (hitInfo != null)
			{
				for (int i = 0; i < hitInfo.Length; i++)
				{
					RaycastHit2D hit = hitInfo[i];
					GameObject target = hit.transform.gameObject;
					T visitor = target.GetComponent<T>();

					if (visitor == null) continue;
					if (visitors.Contains(visitor)) continue;

					visitors.Add(visitor);
					success = true;
				}
			}
			return success;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Handles.Label(transform.position, $"{gameObject.name}");

			Vector2 position = (Vector2)transform.position;
			Vector2 topLeftPoint = position + new Vector2(boundary.Min.x, boundary.Max.y);
			Vector2 topRightPoint = position + new Vector2(boundary.Max.x, boundary.Max.y);
			Vector2 bottomRightPoint = position + new Vector2(boundary.Max.x, boundary.Min.y);
			Vector2 bototmLeftPoint = position + new Vector2(boundary.Min.x, boundary.Min.y);

			Gizmos.color = color;

			Gizmos.DrawRay(topLeftPoint, Vector2.right * boundary.Width);
			Gizmos.DrawRay(topRightPoint, Vector2.down * boundary.Height);
			Gizmos.DrawRay(bottomRightPoint, Vector2.left * boundary.Width);
			Gizmos.DrawRay(bototmLeftPoint, Vector2.up * boundary.Height);

			Handles.Label(new Vector2(topLeftPoint.x + boundary.Width / 2, topLeftPoint.y + 0.5f), $"{boundary.Width}");
			Handles.Label(new Vector2(topRightPoint.x + 0.2f, topLeftPoint.y - boundary.Height / 2), $"{boundary.Height}");

			DrawPoint(topLeftPoint, 0.2f, Color.white);
			DrawPoint(topRightPoint, 0.2f, Color.white);
			DrawPoint(bottomRightPoint, 0.2f, Color.white);
			DrawPoint(bototmLeftPoint, 0.2f, Color.white);
		}

		private void DrawPoint(Vector2 position, float size, Color color)
		{
			Gizmos.color = color;
			Gizmos.DrawWireSphere(position, size);
			Handles.Label(new Vector2(position.x, position.y + 0.5f), $"x: {position.x}, y: {position.y}");
		}
#endif
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Contexts.Game.Components.Fence
{
	public abstract class Fence<T> : MonoBehaviour where T : IFenceVisitor
	{
		[SerializeField] protected Boundary boundary;
		[SerializeField] protected LayerMask targetLayers;
		[SerializeField] protected float fenceWidth = 0.1f;
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

		protected bool CastRay(Vector3 worldPosition, Vector2 direction, float lenght, ref List<T> visitors)
		{
			bool success = false;

			float halfWidth = fenceWidth * 0.5f;
			Vector2 leftRayPosition = new Vector2(worldPosition.x, worldPosition.y);
			Vector2 rightRayPosition = new Vector2(worldPosition.x, worldPosition.y);

			if (lenght > 0)
			{
				if (direction == Vector2.right || direction == Vector2.left)
				{
					leftRayPosition.y += halfWidth;
					rightRayPosition.y -= halfWidth;
				}
				else
				{
					leftRayPosition.x += halfWidth;
					rightRayPosition.x -= halfWidth;
				}
			}

			RaycastHit2D[] leftInfo = Physics2D.RaycastAll(leftRayPosition, direction, lenght, targetLayers);
			RaycastHit2D[] rightInfo = Physics2D.RaycastAll(rightRayPosition, direction, lenght, targetLayers);

			List<RaycastHit2D> hitInfo = leftInfo.ToList();
			hitInfo.AddRange(rightInfo);

			if (hitInfo != null)
			{
				for (int i = 0; i < hitInfo.Count; i++)
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
			Vector2 bottomLeftPoint = position + new Vector2(boundary.Min.x, boundary.Min.y);

			Gizmos.color = color;

			Vector3[] path = new Vector3[4]
			{
				topLeftPoint,
				topRightPoint,
				bottomRightPoint,
				bottomLeftPoint
			};
			ReadOnlySpan<Vector3> readOnlySpan = new ReadOnlySpan<Vector3>(path);
			Gizmos.DrawLineStrip(readOnlySpan, true);

			Handles.Label(new Vector2(topLeftPoint.x + boundary.Width / 2, topLeftPoint.y + 0.5f), $"{boundary.Width}");
			Handles.Label(new Vector2(topRightPoint.x + 0.2f, topLeftPoint.y - boundary.Height / 2), $"{boundary.Height}");

			DrawPoint(topLeftPoint, 0.2f, Color.white);
			DrawPoint(topRightPoint, 0.2f, Color.white);
			DrawPoint(bottomRightPoint, 0.2f, Color.white);
			DrawPoint(bottomLeftPoint, 0.2f, Color.white);
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
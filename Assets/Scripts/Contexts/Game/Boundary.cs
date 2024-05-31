using System;
using UnityEngine;

namespace Contexts.Game
{
	[Serializable]
	public struct Boundary
	{
		public Vector2 Min;
		public Vector2 Max;

		public float Width => Mathf.Abs(Min.x) + Max.x;
		public float Height => Mathf.Abs(Min.y) + Max.y;
	}
}
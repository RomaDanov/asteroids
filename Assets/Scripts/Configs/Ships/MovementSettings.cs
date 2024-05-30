using System;
using UnityEngine;

namespace Configs.Ships
{
	[Serializable]
	public struct MovementSettings
	{
		[SerializeField, Min(0)] private float maxSpeed;
		[SerializeField, Min(0)] private float acceleration;
		[SerializeField, Min(0)] private float torq;
		[SerializeField, Min(0)] private float brakeForce;

		public float MaxSpeed => maxSpeed;
		public float Acceleration => acceleration;
		public float Torq => torq;
		public float BrakeForce => brakeForce;
	}
}
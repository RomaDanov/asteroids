using Configs.Ships;
using System;
using UnityEngine;

namespace Contexts.Game.Components.Movements
{
	public abstract class Movement : MonoBehaviour
	{
		public Action<bool> MoveProccessing;

		protected MovementSettings settings;

		public void ApplySettings(MovementSettings settings)
		{
			this.settings = settings;
		}
	}
}
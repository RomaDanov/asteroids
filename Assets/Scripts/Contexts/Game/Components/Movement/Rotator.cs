using UnityEngine;

namespace Contexts.Game.Components
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField] private float rotationSpeed;

		public void Configure(float rotationSpeed)
		{
			this.rotationSpeed = rotationSpeed;
		}

		private void Update()
		{
			Rotate(rotationSpeed * Time.deltaTime);
		}

		private void Rotate(float speed)
		{
			transform.rotation = Quaternion.AngleAxis(speed, Vector3.forward) * transform.rotation;
		}
	}
}
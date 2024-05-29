using System.Collections.Generic;
using UnityEngine;

namespace Configs.Asteriods
{
	[CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Configs/Asteroids/Config")]
	public class AsteroidConfig : Config
	{
		[Header("Asteroid")]
		[SerializeField] private float maxHealth;
		[SerializeField] private Vector2 moveSpeedRange;
		[SerializeField] private Vector2 rotateSpeedRange;
		[SerializeField] private Asteroid prefab;
		[SerializeField] private AsteroidConfig[] destructionFragments;

		public float MaxHealth => maxHealth;
		public Vector2 MoveSpeedRange => moveSpeedRange;
		public Vector2 RotateSpeedRange => rotateSpeedRange;
		public Asteroid Prefab => prefab;
		public IReadOnlyCollection<AsteroidConfig> DestructionFragments => destructionFragments;
	}
}
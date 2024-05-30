using Contexts.Game.Components.Asteroid;
using System.Collections.Generic;
using UnityEngine;

namespace Configs.Asteriods
{
	[CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Configs/Asteroids/Config")]
	public class AsteroidConfig : Config
	{
		[Space]
		[Header("Asteroid")]
		[SerializeField] private float maxHealth;
		[SerializeField] private float moveSpeed;
		[SerializeField] private float rotateSpeed;
		[SerializeField] private Asteroid prefab;
		[SerializeField] private AsteroidConfig[] destructionFragments;

		public float MaxHealth => maxHealth;
		public float MoveSpeed => moveSpeed;
		public float RotateSpeed => rotateSpeed;
		public Asteroid Prefab => prefab;
		public IReadOnlyCollection<AsteroidConfig> DestructionFragments => destructionFragments;
	}
}
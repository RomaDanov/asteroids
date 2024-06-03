using Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Spawn
{
	public abstract class Spawner : MonoBehaviour
	{
		[Header("Base")]
		[SerializeField] private Config[] configs;
		[SerializeField] private Transform parent;
		[SerializeField] private int prewarmCount;
		[SerializeField] private int maxCount;
		[SerializeField] private float interval;
		[SerializeField] private float spawnRadius;

		private List<GameObject> spawnedObjects = new();
		private float currentTime;

		public abstract GameObject Spawn(Config config, Transform parent);

		private void Start()
		{
			SpawnPrewarm();
			currentTime = Random.Range(0, interval);
		}

		private void Update()
		{
			if (currentTime <= 0)
			{
				ValidateSpawnedObjects();
				if (spawnedObjects.Count >= maxCount) return;
				SpawnToList();
				currentTime = interval;
			}
			else
			{
				currentTime -= Time.deltaTime;
			}
		}

		private void ValidateSpawnedObjects()
		{
			for (int i = 0; i < spawnedObjects.Count; i++)
			{
				GameObject obj = spawnedObjects[i];
				if (obj == null || !obj.activeInHierarchy)
				{
					spawnedObjects.RemoveAt(i);
					i--;
				}
			}
		}

		private void SpawnPrewarm()
		{
			for (int i = 0; i < prewarmCount; i++)
			{
				SpawnToList();
			}
		}

		private void SpawnToList()
		{
			GameObject newGameObject = Spawn(GetRandomConfig(), parent);
			spawnedObjects.Add(newGameObject);
		}

		protected Vector2 GetRandomSpawnPosition()
		{
			return (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
		}

		protected Config GetRandomConfig()
		{
			return configs[Random.Range(0, configs.Length)];
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (!enabled) return;
			Gizmos.DrawWireSphere(transform.position, spawnRadius);
		}
#endif
	}
}
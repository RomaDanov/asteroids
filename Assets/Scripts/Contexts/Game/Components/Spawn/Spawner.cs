using Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Contexts.Game.Components.Spawn
{
	public abstract class Spawner : MonoBehaviour
	{
		[SerializeField] protected Config config;
		[SerializeField] private int prewarmCount;
		[SerializeField] private int maxCount;
		[SerializeField] private float interval;

		private List<GameObject> spawnedObjects = new();
		private float currentTime;

		public abstract GameObject Spawn();

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
			GameObject newGameObject = Spawn();
			spawnedObjects.Add(newGameObject);
		}
	}
}
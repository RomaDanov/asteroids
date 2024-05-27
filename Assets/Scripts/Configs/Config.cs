using UnityEngine;

namespace Configs
{
	public abstract class Config : ScriptableObject, IConfig
	{
		[Header("Base")]
		[SerializeField] private string id;

		public string Id => id;
	}
}
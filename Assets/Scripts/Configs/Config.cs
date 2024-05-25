using UnityEngine;

namespace Configs
{
	public abstract class Config : ScriptableObject, IConfig
	{
		[SerializeField] private string id;

		public string Id => id;
	}
}
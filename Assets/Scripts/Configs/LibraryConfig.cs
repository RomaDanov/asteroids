using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Configs
{
	public abstract class LibraryConfig<T> : ScriptableObject where T : IConfig, new()
	{
		[SerializeField] private T[] items;

		public virtual IReadOnlyCollection<T> Items => items;
	}
}
using ServiceLocator;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataProvider<T> : ServiceInstance where T : ScriptableObject
{
	protected T library;

	public override void PreLoad()
	{
		var libraryType = typeof(T);
		library = AssetDatabase.LoadAssetAtPath<T>(Path.Combine("Assets", "Resources", "Configs", $"{libraryType.Name}.asset"));
	}
}

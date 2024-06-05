using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
	public delegate void SceneLoadedHandler(string sceneName);

	public string sceneName;
	public SceneLoadedHandler Loaded;

	public SceneLoader(string sceneName, LoadSceneMode mode)
	{
		if (SceneManager.GetAllScenes().Any(x => x.name == sceneName))
		{
			Debug.LogError($"Scene {sceneName} already loaded!");
			OnLoaded(null);
			return;
		}

		this.sceneName = sceneName;
		var asyncOp = SceneManager.LoadSceneAsync(sceneName, mode);
		asyncOp.completed += OnLoaded;
	}

	private void OnLoaded(AsyncOperation op)
	{
		Loaded?.Invoke(sceneName);
	}
}

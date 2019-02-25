using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour {
	int lastLoadedSceneIndex = 1;

	void Awake () {
		SetUpSingleton();
	}

	private void SetUpSingleton() {
		if (FindObjectsOfType<SceneLoader>().Length > 1) {
			Destroy(gameObject);
		}
		else {
			DontDestroyOnLoad(gameObject);
		}
	}

	public int getSceneIndex() {
		return SceneManager.GetActiveScene().buildIndex;
	}

	private void _loadScene(int index) {
		lastLoadedSceneIndex = getSceneIndex();
		SceneManager.LoadScene(index);
	}


	public void loadNextScene() {
		int currentSceneIndex = getSceneIndex();
		_loadScene(currentSceneIndex + 1);
	}

	public void loadLastLoadedScene() {
		_loadScene(lastLoadedSceneIndex);
	}

	public void loadFirstScene() {
		_loadScene(0);
	}

	public void loadScene(int index) {
		_loadScene(index);
	}

	public void loadSceneFromName(string name, float delay=0f) {
		if (delay > 0f) {
			StartCoroutine(loadDelayedScene(name, delay));
		}
		else {
			lastLoadedSceneIndex = getSceneIndex();
			SceneManager.LoadScene(name);
		}
	}

	public IEnumerator loadDelayedScene(string name, float delay) {
		yield return new WaitForSeconds(delay);
		loadSceneFromName(name);
	}

	public void quit() {
		Application.Quit();
	}
}

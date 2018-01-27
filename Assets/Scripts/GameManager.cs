using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager instance = null;


  [Header("Game Architecture")]
  public string[] LevelSceneNames = new string[0];
  public int[] LevelSecondsToPrepare = new int[0];

	private int curLevelIndex = 0;


  void Start()
  {
    if (!instance)
    {
      instance = this;
			DontDestroyOnLoad(this.gameObject);

			// TODO actually load on button press
			LoadLevel(0);
    }
    else
    {
      GameObject.Destroy(this);
    }
  }

	public void LoadLevel(int levelIndex)
	{
		this.curLevelIndex = levelIndex;
		SceneManager.LoadSceneAsync(LevelSceneNames[levelIndex]);
	}

  public void OnPlayerDied()
  {
    Debug.LogWarning("Player DIED!!");
  }
}

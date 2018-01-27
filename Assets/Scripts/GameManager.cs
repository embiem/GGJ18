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
  public float[] LevelTransmissionPointsToWin = new float[0];

	private int curLevelIndex = 0;
  private float levelStartTime = 0f;

  private float transmissionPoints = 0;

  public float RemainingSecondsToPrepare {
    get {
      return LevelSecondsToPrepare[curLevelIndex] - (Time.time - levelStartTime);
    }
  }

  public float SecondsSinceStartOfTransmission {
    get {
      return RemainingSecondsToPrepare * -1;
    }
  }

  public float TransmissionPoints
  {
    get
    {
      return transmissionPoints;
    }

    set
    {
      Debug.Log("Transmission Points: " + value);
      transmissionPoints = value;
    }
  }

  void Start()
  {
    if (!instance)
    {
      instance = this;
			DontDestroyOnLoad(this.gameObject);
    }
    else
    {
      GameObject.Destroy(this);
    }
  }

	public void LoadLevel(int levelIndex)
	{
		this.curLevelIndex = levelIndex;
		SceneManager.LoadScene(LevelSceneNames[levelIndex]);
    this.levelStartTime = Time.time;
    this.transmissionPoints = 0;
	}

  public void OnPlayerDied()
  {
    Debug.LogWarning("Player DIED!!");
  }
}

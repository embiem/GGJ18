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
  private bool gameOver = false;
  private string gameOverReason = "";

  public float RemainingSecondsToPrepare {
    get {
      return LevelSecondsToPrepare[CurLevelIndex] - (Time.time - levelStartTime);
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
      if (value >= LevelTransmissionPointsToWin[CurLevelIndex]) {
        this.GameOverReason = "Transmission successfully completed!";
        this.GameOver = true;
      }
      transmissionPoints = value;
    }
  }

  public bool GameOver
  {
    get
    {
      return gameOver;
    }

    set
    {
      gameOver = value;
    }
  }

  public int CurLevelIndex
  {
    get
    {
      return curLevelIndex;
    }

    set
    {
      curLevelIndex = value;
    }
  }

  public string GameOverReason
  {
    get
    {
      return gameOverReason;
    }

    set
    {
      gameOverReason = value;
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
		this.CurLevelIndex = levelIndex;
		SceneManager.LoadScene(LevelSceneNames[levelIndex]);
    this.levelStartTime = Time.time;
    this.transmissionPoints = 0;
    this.GameOver = false;
	}

  public void OnAllTransmissionTowersDied()
  {
    if (!this.GameOver) {
      this.GameOverReason = "All transmission towers destroyed!";
      this.GameOver = true;
    }
  }

  public void OnPlayerDied()
  {
    if (!this.GameOver) {
      this.GameOverReason = "You died!";
      this.GameOver = true;
    }
  }
}

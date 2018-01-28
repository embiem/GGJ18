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

  [Header("refs")]
  public AudioSource AudioStartingSoon;
  public AudioSource AudioStartingTenSec;
  public AudioSource AudioStartingNow;
  public AudioSource AudioTowerDestroyed;
  public AudioSource AudioPlayerDied;
  public AudioSource AudioAllTransmissionTowersDestroyed;
  public AudioSource AudioSuccessful;
  public AudioSource AudioPlayerDeath;


  private int curLevelIndex = 0;
  private float levelStartTime = 0f;

  private bool playedTenSecAudio = false;
  private bool playedStartNowAudio = false;
  private float transmissionPoints = 0;
  private bool gameOver = false;
  private string gameOverReason = "";

  public float RemainingSecondsToPrepare
  {
    get
    {
      var calcedVal = LevelSecondsToPrepare[CurLevelIndex] - (Time.time - levelStartTime);
      if (!playedTenSecAudio && calcedVal < 10)
      {
        AudioStartingTenSec.Play();
        playedTenSecAudio = true;
      }
      if (!playedStartNowAudio && calcedVal < 0)
      {
        AudioStartingNow.Play();
        playedStartNowAudio = true;
      }
      return calcedVal;
    }
  }

  public float SecondsSinceStartOfTransmission
  {
    get
    {
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
      if (!GameOver && value >= LevelTransmissionPointsToWin[CurLevelIndex])
      {
        this.GameOverReason = "Transmission successfully completed!";
        this.GameOver = true;
        AudioSuccessful.Play();
      }
      if (!GameOver)
        transmissionPoints = value;
    }
  }

  public float TransmissionProgress
  {
    get
    {
      return 1.0f * TransmissionPoints / LevelTransmissionPointsToWin[CurLevelIndex];
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

      if (SceneManager.GetActiveScene().name.ToLower() != "menu") {
        AudioStartingSoon.Play();
      }
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
    this.playedTenSecAudio = false;
    this.playedStartNowAudio = false;
    AudioStartingSoon.Play();
  }

  public void OnTransmissionTowerDestroyed()
  {
    AudioTowerDestroyed.Play();
  }

  public void OnAllTransmissionTowersDied()
  {
    if (!this.GameOver)
    {
      this.GameOverReason = "All transmission towers destroyed!";
      this.GameOver = true;
      AudioAllTransmissionTowersDestroyed.Play();
    }
  }

  public void OnPlayerDied()
  {
    if (!this.GameOver)
    {
      this.GameOverReason = "You died!";
      this.GameOver = true;
      AudioPlayerDeath.Play();
            StartCoroutine("PlayDelayedSound");
    }
  }

    IEnumerator PlayDelayedSound()
    {
        yield return new WaitForSeconds(1f);
        AudioPlayerDied.Play();
    }
}

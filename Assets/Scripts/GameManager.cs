using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	void Start () {
		instance = this;
	}

	public void OnPlayerDied()
	{
		Debug.LogWarning("Player DIED!!");
	}
}

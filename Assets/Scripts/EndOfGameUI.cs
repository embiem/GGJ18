using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfGameUI : MonoBehaviour
{
	public GameObject EndOfGamePanel;
	public Text EndOfGameText;
	public Button EndOfGameButton;
	public Text EndOfGameButtonText;

  void Start()
  {
		EndOfGamePanel.SetActive(false);
  }

  void Update()
  {
		if (!EndOfGamePanel.activeSelf && GameManager.instance.GameOver) {
			EndOfGamePanel.SetActive(true);
			EndOfGameText.text = GameManager.instance.GameOverReason;
			EndOfGameButtonText.text = "Play Again";
		}
  }

	public void OnEndOfGameButtonClicked()
	{
		GameManager.instance.LoadLevel(GameManager.instance.CurLevelIndex);
	}
}

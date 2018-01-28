using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstallmentUI : MonoBehaviour
{
	public Text TrapText;
	public Text TurretText;

	private PlaceItem placeItemScript;
	private int[] cachedCounts;

	void Start()
  {
		placeItemScript = FindObjectOfType<PlaceItem>();
		cachedCounts = new int[placeItemScript.defenseItemCounts.Length];
		for (var i = 0; i < cachedCounts.Length; i++)
		{
			cachedCounts[i] = placeItemScript.defenseItemCounts[i];
		}
		TrapText.text = "x " + cachedCounts[0];
		TurretText.text = "x " + cachedCounts[1];
  }

  void Update()
  {
		if (cachedCounts[0] != placeItemScript.defenseItemCounts[0])
		{
			cachedCounts[0] = placeItemScript.defenseItemCounts[0];
			TrapText.text = "x " + cachedCounts[0];
		}
		else if (cachedCounts[1] != placeItemScript.defenseItemCounts[1])
		{
			cachedCounts[1] = placeItemScript.defenseItemCounts[1];
			TurretText.text = "x " + cachedCounts[1];
		}
  }
}

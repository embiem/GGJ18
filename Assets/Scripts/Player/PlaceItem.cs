using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceItem : MonoBehaviour
{
  public GameObject[] defenseItem;
  public string[] defenseItemNames;
  public int[] defenseItemCounts;

  public bool canPlaceItem;

  int currentItemIdx = 0;
  PlayerController player;

  public int CurrentItemIdx
  {
    get
    {
      return currentItemIdx;
    }

    set
    {
      currentItemIdx = value;
    }
  }

  void Start()
  {
    this.player = FindObjectOfType<PlayerController>();
  }

  private void Update()
  {
    if (Input.GetButtonDown("Jump"))
    {
      CurrentItemIdx = (CurrentItemIdx + 1) % defenseItem.Length;
    }

    if (canPlaceItem)
    {
      if (Input.GetButtonDown("Fire2"))
      {
        if (defenseItemCounts[CurrentItemIdx] > 0)
        {
          PlaceDefenseItem(defenseItem[CurrentItemIdx]);
          defenseItemCounts[CurrentItemIdx]--;
        }
      }
    }
  }

  public void PlaceDefenseItem(GameObject item)
  {
    if (item == null)
    {
      Debug.LogWarning("No item to set! Choose an item first!");
    }

    if (item != null)
    {
      Instantiate(item, transform.position + this.player.LookDirection, Quaternion.identity);
    }
  }
}

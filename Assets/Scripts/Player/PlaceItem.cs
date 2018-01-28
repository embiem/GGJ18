using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceItem : MonoBehaviour
{
  public GameObject[] defenseItem;
  public string[] defenseItemNames;

  public int maxItems = 5;

  public bool canPlaceItem;

  int currentItemIdx = 0;
  PlayerController player;

  int itemCount = 0;

  void Start()
  {
    this.player = FindObjectOfType<PlayerController>();
  }

  private void Update()
  {
    if (Input.GetButtonDown("Jump"))
    {
      currentItemIdx = (currentItemIdx + 1) % defenseItem.Length;
    }

    if (canPlaceItem)
    {
      if (Input.GetButtonDown("Fire2"))
      {
        if (itemCount < maxItems)
        {
          PlaceDefenseItem(defenseItem[currentItemIdx]);
          itemCount++;
        }

        if (itemCount == maxItems)
        {
          print("no more items can be placed");
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

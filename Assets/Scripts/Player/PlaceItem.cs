using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceItem : MonoBehaviour
{
    public GameObject[] defenseItem;

    public int maxItems = 5;

    public bool canPlaceItem;

    public Text itemCountDisplay;

    GameObject currentItem;
    PlayerController player;

    int itemCount = 0;

    void Start()
    {
        this.player = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (canPlaceItem)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentItem == null)
                {
                    Debug.LogError("No item to set! Choose an item first!");
                }

                if (itemCount < maxItems && currentItem != null) {
                    PlaceDefenseItem(currentItem);
                    itemCount++;


                    itemCountDisplay.text = "Available Items " + (maxItems - itemCount).ToString();
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
            Debug.LogError("No item to set! Choose an item first!");
        }

        if (item != null) {
            Instantiate(item, transform.position + this.player.LookDirection, Quaternion.identity);
        }

    }

    public void SelectItem(int itemID)
    {
        switch (itemID)
        {
            case 0:
               currentItem = defenseItem[0];
              break;

            case 1:
                currentItem = defenseItem[1];
                break;

        }
    }
}

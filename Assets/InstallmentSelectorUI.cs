using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallmentSelectorUI : MonoBehaviour
{
	public float[] YPositions;

	private int activePositionIdx = 0;

	private float timeBucket = 0f;

  public int ActivePositionIdx
  {
    get
    {
      return activePositionIdx;
    }

    set
    {
			timeBucket = 0f;
      activePositionIdx = value % YPositions.Length;
    }
  }

	private RectTransform myRect;
	private PlaceItem placeItem;

	void Start()
	{
		myRect = GetComponent<RectTransform>();
		placeItem = FindObjectOfType<PlaceItem>();
	}

	void Update()
	{
		if (this.ActivePositionIdx != placeItem.CurrentItemIdx) {
			this.ActivePositionIdx = placeItem.CurrentItemIdx;
		}

		timeBucket += Time.deltaTime * 2f;

		myRect.anchoredPosition = new Vector2(
			myRect.anchoredPosition.x,
			Mathf.Lerp(YPositions[Mathf.Abs(ActivePositionIdx - 1) % YPositions.Length], YPositions[ActivePositionIdx], timeBucket)
		);
	}
}

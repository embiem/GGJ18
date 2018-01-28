using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
  [Header("Balancing Vals")]
  public bool IsLeftSpawn = true;
  public float TargetXPos = 0f;
	public float AnimationSpeed = 2f;
  public float SpawnEveryXSeconds = 9f;
  public Vector2 SpawnRandomRange = new Vector2(-2f, 2f);
	public float YPos = 0.5f;

  [Header("Refs")]
  public GameObject[] LeftPrefabs;
  public GameObject[] RightPrefabs;

  private float nextTimeSpawned = 0f;
  private GameObject curSpawnedNPC;

  void Start()
  {
    nextTimeSpawned = Time.time + SpawnEveryXSeconds + Random.Range(SpawnRandomRange.x, SpawnRandomRange.y);
  }

  // Update is called once per frame
  void Update()
  {
    if (Time.time > nextTimeSpawned && curSpawnedNPC == null)
    {
      nextTimeSpawned = Time.time + SpawnEveryXSeconds + Random.Range(SpawnRandomRange.x, SpawnRandomRange.y);
      curSpawnedNPC = Instantiate(IsLeftSpawn ?
				LeftPrefabs[Random.Range(0, LeftPrefabs.Length - 1)] :
				RightPrefabs[Random.Range(0, RightPrefabs.Length - 1)],
				transform.position + Vector3.up * YPos, Quaternion.identity
			);
    }

		if (curSpawnedNPC)
		{
			var curX = curSpawnedNPC.transform.position.x;
			if (IsLeftSpawn && curX > TargetXPos)
			{
				curSpawnedNPC.transform.Translate(-AnimationSpeed * Time.deltaTime, 0f, 0f);
			}
			else if (IsLeftSpawn && curX < TargetXPos)
			{
				Destroy(curSpawnedNPC.gameObject);
			}
			else if (!IsLeftSpawn && curX < TargetXPos)
			{
				curSpawnedNPC.transform.Translate(AnimationSpeed * Time.deltaTime, 0f, 0f);
			}
			else if (!IsLeftSpawn && curX > TargetXPos)
			{
				Destroy(curSpawnedNPC.gameObject);
			}
		}
  }
}

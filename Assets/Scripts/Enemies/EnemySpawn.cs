using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
  [Header("Balancing Values")]
  public AnimationCurve SpawnOverTime;
  [Tooltip("How quickly the spawn bursts will increase in number. Lower value means it reaches maximum spawnBurstAmount earlier.")]
  public float SecondsUntilMaxSpawnBurstAmount = 100f;
  public float SpawnCooldown = 10f;
	public Vector2Int RandomSpawnCooldownRange = new Vector2Int(-2, 2);

  public int MaxSpawnBurstAmount = 10;
  public Vector2Int RandomBurstAmountRange = new Vector2Int(-1, 1);

  [Header("Ref")]
  public Enemy EnemyPrefab;

  private float lastSpawnTime = 0f;
	private float curSpawnCooldown = 0f;


  void Update()
  {
    if (Time.time - lastSpawnTime > curSpawnCooldown)
    {
      var curVal = SpawnOverTime.Evaluate(Time.time / SecondsUntilMaxSpawnBurstAmount);
      var burstAmount = Mathf.CeilToInt(MaxSpawnBurstAmount * curVal) + Random.Range(RandomBurstAmountRange.x, RandomBurstAmountRange.y);

      for (var i = 0; i < burstAmount; i++)
      {
        Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
      }

      lastSpawnTime = Time.time;
			curSpawnCooldown = SpawnCooldown + Random.Range(RandomSpawnCooldownRange.x, RandomSpawnCooldownRange.y);
    }
  }
}

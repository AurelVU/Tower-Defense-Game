using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWaveGenerator : AbstractWaveGenerator
{
    public int countEnemies;
    public float delay = 0.5f;
    public override void generateWave() {
        Debug.Log("GenerateWave");
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < countEnemies; i++)
        {
            for (int j = 0; j < enemies.Count; j++)
            {
                spawnEnemy(enemies[j]);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}

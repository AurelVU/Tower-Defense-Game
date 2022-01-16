using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractWaveGenerator : MonoBehaviour {
    public List<Transform> enemies;
    public EnemyFactoryController enemyFactoryController;

    public void spawnEnemy(Transform enemy) {
        enemyFactoryController.SpawnEnemy(enemy);
    }

    public abstract void generateWave();
}
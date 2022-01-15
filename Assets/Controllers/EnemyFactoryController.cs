using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFactoryController : MonoBehaviour
{
    public delegate void DamagePlayer(int damage);
    public event DamagePlayer? onDamagePlayer;
    public delegate void WaveFinish(int waveIndex);
    public event WaveFinish? onWaveFinished;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public Transform spawnPoint;
    public List<Transform> enemies;
    // public Transform enemy;
    // public Transform enemy2;
    // public Transform enemy3;
    public float delay = 0.5f;

    public Text waveCountdownText;
    private int waveIndex = 0;
    private int maxWaveIndex = 5;
    public static EnemyFactoryController instance;
    public bool isLastWave {get { return maxWaveIndex == waveIndex - 1; } private set {}}
    void Start()
    {
        instance = new EnemyFactoryController();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveIndex < maxWaveIndex)
        {
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime;

            waveCountdownText.text = $"Волна {waveIndex} / {maxWaveIndex}\nДо следующей волны {Mathf.Round(countdown).ToString()} с.";
        } else {
            waveCountdownText.text = "Силы противника\nисчерпаны";
        }
    }

    void finishEnemy(int damage) {
        if (onDamagePlayer != null)
            onDamagePlayer(damage);
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            for (int j = 0; j < enemies.Count; j++)
            {
                SpawnEnemy(enemies[j]);
                yield return new WaitForSeconds(delay);
            }
        }
        if (onWaveFinished != null)
            onWaveFinished(waveIndex);
    }

    void SpawnEnemy(Transform enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}

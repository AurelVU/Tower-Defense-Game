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

    public List<GameObject> waves;
    //public float delay = 0.5f;

    public Text waveCountdownText;
    private int waveIndex = 0;
    //private int maxWaveIndex = 5;
    public static EnemyFactoryController instance;
    public bool isLastWave {get { return waves.Count == waveIndex - 1; } private set {}}
    void Start()
    {
        instance = new EnemyFactoryController();

        for (int i = 0; i < waves.Count; i++) {
            waves[i].GetComponent<AbstractWaveGenerator>().enemies = enemies;
            waves[i].GetComponent<AbstractWaveGenerator>().enemyFactoryController = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (waveIndex < waves.Count)
        {
            if (countdown <= 0f)
            {

                Debug.Log($"{countdown}");
                if (onWaveFinished != null)
                    onWaveFinished(waveIndex);
                
                var wave = Instantiate<GameObject>(waves[waveIndex]);
                wave.GetComponent<AbstractWaveGenerator>().generateWave();
                //StartCoroutine(SpawnWave());
                waveIndex++;
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime;

            waveCountdownText.text = $"Волна {waveIndex} / {waves.Count}\nДо следующей волны {Mathf.Round(countdown).ToString()} с.";
        } else {
            waveCountdownText.text = "Силы противника\nисчерпаны";
        }
    }

    void finishEnemy(int damage) {
        if (onDamagePlayer != null)
            onDamagePlayer(damage);
    }

    public void SpawnEnemy(Transform enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}

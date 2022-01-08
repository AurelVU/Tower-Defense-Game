using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFactoryController : MonoBehaviour
{
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public Transform spawnPoint;
    public Transform enemy;
    public float delay = 0.5f;

    public Text waveCountdownText;
    private int waveIndex = 0;
    private int maxWaveIndex = 5;

    // Start is called before the first frame update
    void Start()
    {
        
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

            waveCountdownText.text = $"До следующей волны {Mathf.Round(countdown).ToString()} с.";
        } else {
            waveCountdownText.text = "Силы противника\nисчерпаны";
        }
    }

    IEnumerator SpawnWave ()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnEnemy ()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}

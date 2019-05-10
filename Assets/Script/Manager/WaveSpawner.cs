using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {


    public static int EnemiesAlive = 0;

    public Wave[] waves; 

    public Transform spawnPoint;

    public float timebetweenWaves = 5f;


    private float countdown = 5f;
    private int WaveIndex = 0;

    public Text waveCountdownTimer;

    public GameManager gameManager;


	void Update()
    {
        

        if (EnemiesAlive > 0)
            return;

        if (WaveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timebetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownTimer.text = string.Format("{0:00.00}", countdown);
	}
	
	IEnumerator SpawnWave ()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[WaveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        WaveIndex++;
	}

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position,spawnPoint.rotation);
    }
}

using UnityEngine;
using System.Collections;

public class EnemySpawner_bw : MonoBehaviour {

    public GameObject diver_prefab;
    public GameObject sprinter_prefab;
    public GameObject turret_prefab;
    public int numOfDifferentEnemies;
    public GameObject[] enemy_prefabs;
    public int spawnForDebug = 0;

    public bool others_ready = true;

    private int enemy_threshold = 10; // Controls how many enemies can be spawned in a wave
    public bool spawner_ready = true;
    public bool doing_end_of_wave_processing = false;
    public int enemies_active = 0;

    private ShipController ship_controller;
    public asc_bw ab_sel_controller;
    private float post_wave_actions = 0;

    public float time_between_waves_length;
    public float time_between_waves_timer;

	// Use this for initialization
	void Start () {
        ship_controller = GameObject.Find("Ship").GetComponent<ShipController>();
        ab_sel_controller = GameObject.Find("bwPicker").GetComponent<asc_bw>();

        numOfDifferentEnemies = 3;
        enemy_prefabs = new GameObject[numOfDifferentEnemies];
        enemy_prefabs[0] = diver_prefab;
        enemy_prefabs[1] = sprinter_prefab;
        enemy_prefabs[2] = turret_prefab;

        time_between_waves_timer = time_between_waves_length;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (enemies_active == 0 && !doing_end_of_wave_processing) doEndOfWaveProcessing();
        if (spawner_ready && others_ready) time_between_waves_timer -= Time.deltaTime;
        if (time_between_waves_timer < 0) spawnNextWave();
	}

    Vector3 chooseStartPosition()
    {
        float rand = Random.Range(0, 92);
        if(rand >= 32)
        {
            rand = Random.Range(-15, 15);
            if(Random.value >.5)
            {
                return new Vector3(rand, 10, 0);
            }
            else
            {
                return new Vector3(rand, -7, 0);
            }
        }
        else
        {
            rand = Random.Range(-7, 10);
            if (Random.value > .5)
            {
                return new Vector3(-15,rand, 0);
            }
            else
            {
                return new Vector3(15,rand, 0);
            }
        }
    }

    void spawnNextWave()
    {
        spawner_ready = false;
        time_between_waves_timer = time_between_waves_length;
        doing_end_of_wave_processing = false;
        int threshold_copy = enemy_threshold;
        while(threshold_copy >= 2)
        {
            threshold_copy -= spawnEnemy(threshold_copy);
        }
    }

    int spawnEnemy(int threshold)
    {
        int enemy_value = 0;

        // Eventually add some random selection here when more enemies are implemented

        GameObject enemy;

        // Random spawning with increasing difficulty

        int chosen_enemy_type = Mathf.FloorToInt(Random.Range(0f, numOfDifferentEnemies - 0.01f));
        switch (chosen_enemy_type)
        {
            case 0:
                enemy = Instantiate(enemy_prefabs[chosen_enemy_type], chooseStartPosition(), Quaternion.identity) as GameObject;
                enemy_value = 2;
                break;
            case 1:
                enemy = Instantiate(enemy_prefabs[chosen_enemy_type], chooseStartPosition(), Quaternion.identity) as GameObject;
                enemy_value = 2;
                break;
            case 2:
                enemy = Instantiate(enemy_prefabs[chosen_enemy_type], chooseStartPosition(), Quaternion.identity) as GameObject;
                enemy_value = 4;
                break;
        }

        // Debug Spawning

        /*if(spawnForDebug < numOfDifferentEnemies)
            enemy = Instantiate(enemy_prefabs[spawnForDebug], chooseStartPosition(), Quaternion.identity) as GameObject;
        else
            enemy = Instantiate(enemy_prefabs[0], chooseStartPosition(), Quaternion.identity) as GameObject;
        enemy_value = 2;*/

        if (enemy_value > 0) enemies_active++;
        return enemy_value;
    }

    public void loseEnemy()
    {
        enemies_active--;
    }

    public void holdWave()
    {
        others_ready = false;
    }

    public void releaseWave()
    {
        others_ready = true;
    }

    public void doEndOfWaveProcessing()
    {
        doing_end_of_wave_processing = true;
        enemy_threshold = Mathf.FloorToInt(enemy_threshold * 1.2f);

        if(ship_controller.energy_collected > ship_controller.energy_cap)
        {
            ab_sel_controller.pickNewAbility();
            post_wave_actions++;
        }

        spawner_ready = true;
    }
}

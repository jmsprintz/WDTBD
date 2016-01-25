using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public GameObject diver_prefab;
    public GameObject sprinter_prefab;
    public GameObject turret_prefab;
    public GameObject spinner_prefab;
    public GameObject beam_prefab;
    public int numOfDifferentEnemies;
    public int spawnForDebug = 0;

    public bool others_ready = true;

    private int num_beams = 0;
    private int enemy_threshold = 10; // Controls how many enemies can be spawned in a wave
    public bool spawner_ready = true;
    public bool doing_end_of_wave_processing = false;
    public int enemies_active = 0;

    private ShipController ship_controller;
    public AbilitySelectionController ab_sel_controller;
    private float post_wave_actions = 0;

    public float time_between_waves_length;
    public float time_between_waves_timer;

    //bwPicker stuff
    List<GameObject> unusedEnemies = new List<GameObject>();
    public List<GameObject> availEnemies = new List<GameObject>();

    public static EnemySpawner S;

    void Awake()
    { S = this; }

	// Use this for initialization
	void Start () {
        ship_controller = GameObject.Find("Ship").GetComponent<ShipController>();
        ab_sel_controller = GameObject.Find("PanelBG").GetComponent<AbilitySelectionController>();

        availEnemies.Add(diver_prefab);
        availEnemies.Add(sprinter_prefab);
        availEnemies.Add(turret_prefab);
        availEnemies.Add(beam_prefab);
        //availEnemies.Add(spinner_prefab);






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
            rand = Random.Range(-13, 13);
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
            rand = Random.Range(-5, 8);
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
        num_beams = 0;
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

        int chosen_enemy_type = Mathf.FloorToInt(Random.Range(0f, availEnemies.Count - 0.01f));
        switch (chosen_enemy_type)
        {
            case 0:
                enemy = Instantiate(availEnemies[chosen_enemy_type], chooseStartPosition(), Quaternion.identity) as GameObject;
                enemy_value = 2;
                break;
            case 1:
                enemy = Instantiate(availEnemies[chosen_enemy_type], chooseStartPosition(), Quaternion.identity) as GameObject;
                enemy_value = 2;
                break;
            case 2:
                enemy = Instantiate(availEnemies[chosen_enemy_type], chooseStartPosition(), Quaternion.identity) as GameObject;
                enemy_value = 4;
                break;
            case 3:
                if (num_beams < 5)
                {
                    num_beams++;
                    enemy = Instantiate(availEnemies[chosen_enemy_type], chooseStartPosition(), Quaternion.identity) as GameObject;
                    enemy_value = 6;
                }
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
        if ( !ShipController.S.ded)
            Skeeps.S.curScore++;
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

        if(ship_controller.energy_collected > ship_controller.energy_cap && post_wave_actions < 4)
        {
            ab_sel_controller.pickNewAbility();
            post_wave_actions++;
        }

        spawner_ready = true;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BeamEnemy : MonoBehaviour
{

    private GameObject player_ship;
    private EnemySpawner enemy_spawner;
    public List<GameObject> items;
    bool alive = true;
    public float delay = 4;
    public float range = 1;
    public float length = 1.5f;
    public GameObject beamPreFab;
    public GameObject beam;
    public GameObject laserPrefab;

    // Use this for initialization
    void Start()
    {
        delay = Random.Range(delay - range, delay + range);
        player_ship = GameObject.Find("Ship");
        enemy_spawner = GameObject.Find("Main Camera").GetComponent<EnemySpawner>();

        Invoke("showLaser", delay - 1);
        Invoke("DelayedFire", delay);
        transform.position = new Vector3(Random.Range(-14, 14), 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }

    void DelayedFire()
    {
        beam = Instantiate(beamPreFab, new Vector3(transform.position.x,transform.position.y,1), Quaternion.identity) as GameObject;
        Invoke("Die", 2.25f);
    }

    void Die()
    {
       
                EnemySpawner.S.loseEnemy();
                Destroy(gameObject);
            
    }

    void showLaser()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
    }


    

    

    
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SprinterController : MonoBehaviour
{

    public float speed = .3f;
    public float delayBeforeAttack = .75f;
    public GameObject energy;
    public GameObject enemy_explode;

    private GameObject player_ship;
    private EnemySpawner enemy_spawner;
    private float x_min = -14f;
    private float x_max = 14f;
    private float y_min = -7f;
    private float y_max = 9f;
    float leeway = 4;
    bool canMove = true;
    bool spawned = false;

    public List<GameObject> items;
    Vector3 target;
    Vector3 spawnPos;
    bool alive = true;

    // Use this for initialization
    void Start()
    {
        speed = Random.Range(.22f, .4f);
        delayBeforeAttack = Random.Range(.4f, .8f);
        player_ship = GameObject.Find("Ship");
        enemy_spawner = GameObject.Find("Main Camera").GetComponent<EnemySpawner>();
        foreach (Transform child in transform)
        {
            items.Add(child.gameObject);
        }
        items[0].SetActive(!ShipController.S.inv);
        items[1].SetActive(ShipController.S.inv);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.y > 15 || gameObject.transform.position.y < -15
            || gameObject.transform.position.x > 20 || gameObject.transform.position.x < -20)
            OnLeave();
        items[0].SetActive(!ShipController.S.inv);
        items[1].SetActive(ShipController.S.inv);
        if (canMove)
        {
            Vector3 pos = transform.position;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed * (Time.fixedDeltaTime * 50));
            if (ShipController.freezer == true)
            {
                transform.position = pos;
            }
            transform.LookAt(target, Vector3.left);

            float curx = transform.position.x;
            float cury = transform.position.y;
           
            if(transform.position == target )
            {
                OnArrive();
            }
            if (!spawned)
            {
                canMove = false;
                spawned = true;
                OnSpawn();
            }
        }
       
    }

    void OnSpawn()
    {
           
          target = player_ship.transform.position;
          target.z = transform.position.z;
          spawnPos = transform.position;
          Invoke("StartMoving", delayBeforeAttack);
          transform.LookAt(target, Vector3.left);
    }
    void StartMoving()
    {
        canMove = true;
    }

    void OnLeave()
    {
        if(alive)
        {

            enemy_spawner.loseEnemy();
            GameObject energy_obj = Instantiate(energy, spawnPos, Quaternion.identity) as GameObject;
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
        
    }

    void OnArrive()
    {
        if (alive)
        {
            alive = false;
            EnemySpawner.S.loseEnemy();
            GameObject energy_obj = Instantiate(energy, transform.position, Quaternion.identity) as GameObject;
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }

    void Laser()
    {
        if (alive)
        {
            alive = false;
            EnemySpawner.S.loseEnemy();
            GameObject energy_obj = Instantiate(energy, transform.position, Quaternion.identity) as GameObject;
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }

    void Beam()
    {
        if (alive)
        {
            alive = false;
            EnemySpawner.S.loseEnemy();
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }

    void Ship()
    {
        if (alive)
        {

            alive = false;
            EnemySpawner.S.loseEnemy();
            // GameObject energy_obj = Instantiate(energy, spawnPos, Quaternion.identity) as GameObject;
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }

    void Shield()
    {
        if (alive)
        {
            alive = false;
            EnemySpawner.S.loseEnemy();
            GameObject energy_obj = Instantiate(energy, transform.position, Quaternion.identity) as GameObject;
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }
}
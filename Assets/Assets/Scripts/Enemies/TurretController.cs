using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurretController : MonoBehaviour
{

    public float speed = .3f;
    public float missleSpeed = 10;
    public float delayBeforeAttack = .75f;
    public GameObject energy;
    public GameObject laser;
    private GameObject player_ship;
    private EnemySpawner enemy_spawner;
    private float x_min = -14f;
    private float x_max = 14f;
    private float y_min = -7f;
    private float y_max = 9f;

    public GameObject enemy_explode;
    
    float leeway = 4;
    bool canShoot = true;
    bool spawned = false;
    bool moving_in;

    public List<GameObject> items;
    Vector3 target;
    Vector3 endPos;

    int frames = 0;
    public int framesToWaitToStart = 25;
     public int framesBetween = 40;
    bool alive = true;
    // Use this for initialization
    void Start()
    {
        player_ship = GameObject.Find("Ship");
        enemy_spawner = GameObject.Find("Main Camera").GetComponent<EnemySpawner>();
        foreach (Transform child in transform)
        {
            items.Add(child.gameObject);
        }
        frames = framesBetween - framesToWaitToStart;
        items[0].SetActive(!ShipController.S.inv);
        items[1].SetActive(ShipController.S.inv);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.y > 15 || gameObject.transform.position.y < -15
            || gameObject.transform.position.x > 20 || gameObject.transform.position.x < -20)
            OnLeave();
        if (moving_in)
        {
            Vector3 pos = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, endPos, 0.1f * (50 * Time.fixedDeltaTime));
            if (ShipController.freezer == true)
            {
                transform.position = pos;
            }
            if (transform.position == endPos)
            {
                moving_in = false;
                canShoot = true;
            }
            transform.LookAt(endPos, Vector3.left);
        }
        else
        {
            frames++;
            if (frames % framesBetween == 0)
            {
                Vector2 xy = new Vector2(player_ship.transform.position.x - transform.position.x, player_ship.transform.position.y - transform.position.y);
                xy = xy.normalized;
                float x = xy.x;
                float y = xy.y;
                Vector3 input = new Vector3(x, y);
                input *= missleSpeed;
                Vector3 offset = new Vector3(transform.position.x + (.2f * x), transform.position.y + (.2f * y));

                if (ShipController.freezer == false)
                {
                    GameObject laser_clone = Instantiate(laser, offset, Quaternion.identity) as GameObject;
                    laser_clone.GetComponent<Rigidbody>().velocity = input;
                }
            }
            items[0].SetActive(!ShipController.S.inv);
            items[1].SetActive(ShipController.S.inv);
            if (!spawned)
            {
                canShoot = false;
                spawned = true;
                OnSpawn();
            }
            transform.LookAt(player_ship.transform.position, Vector3.left);
        }
    }

    void OnSpawn()
    {

        Vector3 pos = transform.position;
        float distance_to_move = Random.Range(3, 15);
        if(pos.x > 14)
        {
            // Go left
            endPos = new Vector3(transform.position.x - distance_to_move, transform.position.y);
        }else if (pos.x < -14)
        {
            // Go right
            endPos = new Vector3(transform.position.x + distance_to_move, transform.position.y);
        }
        else if (pos.y > 9)
        {
            // Go down
            endPos = new Vector3(transform.position.x, transform.position.y - distance_to_move);
        }
        else
        {
            // Go up
            endPos = new Vector3(transform.position.x, transform.position.y + distance_to_move);
        }
        //pos.x = Random.Range(-13f, 13f);
        //pos.y = Random.Range(-5.5f, 8);
        //transform.position = pos;
        //canShoot = true;
        moving_in = true;
        //Invoke("StartMoving", delayBeforeAttack);
        transform.LookAt(endPos, Vector3.left);
    }
    void StartMoving()
    {
        canShoot = true;
    }

    void OnLeave()
    {
        if (alive)
        {
            alive = false;
            EnemySpawner.S.loseEnemy();
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            // GameObject energy_obj = Instantiate(energy, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }


    void Laser()
    {
        if(alive)
        {
            alive = false;
            EnemySpawner.S.loseEnemy();
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            GameObject energy_obj = Instantiate(energy, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }

    void Ship()
    {
        if (alive)
        {
            alive = false;
            EnemySpawner.S.loseEnemy();
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            // GameObject energy_obj = Instantiate(energy, spawnPos, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }

    void Shield()
    {
        if (alive)
        {
            alive = false;
            enemy_spawner.loseEnemy();
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            GameObject energy_obj = Instantiate(energy, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }

    void Beam()
    {
        if (alive)
        {
            alive = false;
            GameObject expl = Instantiate(enemy_explode, transform.position, Quaternion.identity) as GameObject;
            EnemySpawner.S.loseEnemy();
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DiverController : MonoBehaviour
{
    public GameObject energy;
    public GameObject enemy_explode;

    private GameObject player_ship;
    private EnemySpawner enemy_spawner;
    public List<GameObject> items;
    bool alive = true;
    public float speed;

    // Use this for initialization
    void Start()
    {
        speed = Random.Range(.07f, .15f);
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
        Vector3 pos = transform.position;
        Vector3 target = player_ship.transform.position;
        if (paddle)
        {
            target.x = pos.x + 10 * paddleVector.x;
            target.y = pos.y + 10 * paddleVector.y;
        }
        target.z = transform.position.z;
        transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed * (Time.fixedDeltaTime * 50));
        transform.LookAt(target, Vector3.left);
        if (ShipController.freezer == true)
        {
            transform.position = pos;
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

    void OnLeave()
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

    public bool paddle = false;
    Vector2 paddleVector;
    public void Paddle(Vector2 norm)
    {
        paddle = true;
        paddleVector = norm;
    }
}

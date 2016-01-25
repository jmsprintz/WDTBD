using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpinnerController : MonoBehaviour {
    public GameObject orb;
    public GameObject energy;
    GameObject shot;
    Vector3 pos;
    float x, y;
    public float a, b;
    bool spin = false;
    public float ang;
    float theta = 0f;

    private GameObject player_ship;
    private EnemySpawner enemy_spawner;
    public List<GameObject> items;


    EnemySpawner es;
    bool alive = true;

	// Use this for initialization
	void Start () {
        //transform.rotation = Quaternion.Euler(0, 0, 90);
        player_ship = GameObject.Find("Ship");
        spin = false;
        move();
        es = GameObject.Find("Main Camera").GetComponent<EnemySpawner>();
        foreach (Transform child in transform)
        {
            items.Add(child.gameObject);
        }
        items[0].SetActive(!ShipController.S.inv);
        items[1].SetActive(ShipController.S.inv);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        ang = transform.rotation.z;
        if (gameObject.transform.position.y > 15 || gameObject.transform.position.y < -15
            || gameObject.transform.position.x > 20 || gameObject.transform.position.x < -20)
            OnLeave();
        items[0].SetActive(!ShipController.S.inv);
        items[1].SetActive(ShipController.S.inv);
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 5);
        if (transform.position == pos) spin = true;
        else spin = false;
        if(spin == true)
        {
            theta = theta + 5f;
            transform.rotation = Quaternion.Euler(0, 0, theta);
            if (theta % 5f == 0) shoot();
            if (theta >= 720f)
            {
                theta = 0f;
                spin = false;
                move();
            }
        }
    }

    void move()
    {
        x = -12f + 24f * Random.value;
        y = -9f + 19f * Random.value;
        pos = new Vector3(x, y, 0f);
    }

    void shoot()
    {
        shot = Instantiate(orb, transform.position, Quaternion.identity) as GameObject;
        a = 10f * Mathf.Sin(theta);
        b = 10f * Mathf.Cos(theta);
    //    if (transform.rotation.z > 0) b = -b;
        shot.GetComponent<Rigidbody>().velocity = new Vector3(a, b, 0f);

    }

    void Laser()
    {
        if(alive)
        {
            alive = false;
            es.loseEnemy();
            GameObject energy_obj = Instantiate(energy, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
        
    }

    void Ship()
    {
        if(alive)
        {
            alive = false;
            es.loseEnemy();
            Destroy(gameObject);
        }
        
    }

    void OnLeave()
    {
        if (alive)
        {
            alive = false;
            es.loseEnemy();
            Destroy(gameObject);
        }

    }

    void Shield()
    {
        if (alive)
        {
            alive = false;
            es.loseEnemy();
            GameObject energy_obj = Instantiate(energy, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }

    }

    /*void OnCollisionEnter(Collision coll)
    {
        Debug.Log("Name: " + coll.gameObject.name);
        if (coll.gameObject.name == "Ship")
        {
            ShipController.health--;
     //       enemy_spawner.loseEnemy();
            Destroy(gameObject);
        }
    }*/
}

using UnityEngine;
using System.Collections;

public class OrbController : MonoBehaviour {

    Vector3 pos;
    EnemySpawner es;
    public GameObject energy;
    private bool alive = true; 

	// Use this for initialization
	void Start () {
        es = GameObject.Find("Main Camera").GetComponent<EnemySpawner>();
    }
	
	// Update is called once per frame
	void Update () {
        if (ShipController.freezer == true)
        {
            transform.position = pos;
        }
        if (transform.position.y < -10) Destroy(gameObject);
        if (transform.position.y > 10) Destroy(gameObject);
        if (transform.position.x < -13.5) Destroy(gameObject);
        if (transform.position.x > 13.5) Destroy(gameObject);
        pos = transform.position;
    }

    /*void OnCollisionEnter(Collision coll)
    {
        Debug.Log("Name: " + coll.gameObject.name);
        if (coll.gameObject.name == "Ship")
        {
            ShipController.health--;
          //  enemy_spawner.loseEnemy();
            Destroy(gameObject);
        }
    }*/

    void Ship()
    {
        Destroy(gameObject);
    }

    void Laser()
    {
       
    }
    void Beam()
    {
        if (alive)
        {
            alive = false;
            //EnemySpawner.S.loseEnemy();
            Destroy(gameObject);
        }
    }

    void Shield()
    {
        Destroy(gameObject);
    }
}

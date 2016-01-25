using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {

    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y > 15 || gameObject.transform.position.y < -15 
            || gameObject.transform.position.x > 20 || gameObject.transform.position.x < -20) Destroy(gameObject);
	}

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject != null)
        {
            coll.gameObject.SendMessage("Laser");
            Destroy(gameObject);
        }
    }
}
